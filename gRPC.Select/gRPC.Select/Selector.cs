using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using gRPC.Select.CompareConditions;
using gRPC.Select.Exceptions;
using gRPC.Select.Interface;
using gRPC.Select.LogicConditions;
using gRPC.Select.PropertyConverters;
using gRPC.Select.Tools;
using GRPC.Selector;

namespace gRPC.Select
{
    public class Selector : ISelector
    {
        private readonly ICompareConditionStrategy _compareConditionStrategy;
        private readonly ILogicConditionStrategy _logicConditionStrategy;
        private readonly IValueConverterStrategy _valueConverterStrategy;

        public Selector() : this(new CompareConditionStrategy(), new LogicConditionStrategy(),
            new ValueConverterStrategy())
        {
        }

        public Selector(ICompareConditionStrategy compareConditionStrategy,
            ILogicConditionStrategy logicConditionStrategy,
            IValueConverterStrategy valueConverterStrategy)
        {
            _compareConditionStrategy = compareConditionStrategy;
            _logicConditionStrategy = logicConditionStrategy;
            _valueConverterStrategy = valueConverterStrategy;
        }

        public IQueryable<TModel> Apply<TModel>(IQueryable<TModel> queryableData,
            SelectConditionPack selectConditionPack)
        {
            var _parameter = Expression.Parameter(typeof(TModel), "x");
            var _expression = CreateExpressionFromSelectionPack(selectConditionPack, _parameter);
            return SelectQueryByExpression(queryableData, _expression, _parameter);
        }


        public IQueryable<TModel> Apply<TModel>(IQueryable<TModel> queryableData, SelectCondition selectCondition)
        {
            var _parameter = Expression.Parameter(typeof(TModel), "x");
            var _expression = CreateExpressionFromCondition(_parameter, selectCondition);

            return SelectQueryByExpression(queryableData, _expression, _parameter);
        }

        public IQueryable<TModel> Apply<TModel>(IQueryable<TModel> queryableData, SelectRequest selectRequest)
        {
            var _queryableData = selectRequest.RootSelectConditionCase switch
            {
                SelectRequest.RootSelectConditionOneofCase.None => queryableData,
                SelectRequest.RootSelectConditionOneofCase.SelectCondition => Apply(queryableData,
                    selectRequest.SelectCondition),
                SelectRequest.RootSelectConditionOneofCase.SelectConditionPack => Apply(queryableData,
                    selectRequest.SelectConditionPack),
                _ => throw new ArgumentOutOfRangeException(nameof(selectRequest.RootSelectConditionCase),
                    selectRequest.RootSelectConditionCase, "Unexpected value")
            };

            if (selectRequest.SelectLines.NotNullOrEmpty())
            {
               _queryableData = FilterRowsByIndex(_queryableData, selectRequest.SelectLines);
            }

            return _queryableData;
        }

        private IQueryable<TModel> FilterRowsByIndex<TModel>(IQueryable<TModel> queryableData, SelectLines selectRequestSelectLines)
        {
            var _returnQuery = queryableData;
            int _from = 0;
            if (selectRequestSelectLines.From > 0)
            {
                _from = (int) selectRequestSelectLines.From - 1;
                _returnQuery = _returnQuery.Skip(_from);
            }

            if (selectRequestSelectLines.Till > 0)
            {
                int _skip = (int) selectRequestSelectLines.Till - _from;
                _returnQuery = _returnQuery.Take(_skip);
            }

            return _returnQuery;
        }

        private static IQueryable<TModel> SelectQueryByExpression<TModel>(IQueryable<TModel> queryableData,
            Expression expression,
            ParameterExpression parameter)
        {
            MethodCallExpression _whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] {queryableData.ElementType},
                queryableData.Expression,
                Expression.Lambda<Func<TModel, bool>>(expression, parameter));

            return queryableData.Provider.CreateQuery<TModel>(_whereCallExpression);
        }

        private Expression CreateExpressionFromCondition(ParameterExpression parameterExpression,
            SelectCondition selectCondition)
        {
            var _expressionBuilder = _compareConditionStrategy.GetExpressionBuilder(selectCondition.Condition);
            Expression _member = string.IsNullOrEmpty(selectCondition.PropertyName)
                ? (Expression) parameterExpression
                : Expression.Property(parameterExpression, selectCondition.PropertyName);

            var _constant = Expression.Constant(Convert.ChangeType(selectCondition.Value, _member.Type));

            ConvertProperty(selectCondition, ref _member);

            return _expressionBuilder.Build(_member, _constant);
        }

        private Expression CreateExpressionFromSelectionPack(SelectConditionPack selectConditionPack,
            ParameterExpression parameter)
        {
            var _expressionBuilder =
                _logicConditionStrategy.GetExpressionBuilder(selectConditionPack.JoinCondition);
            Expression _finalExpression = _expressionBuilder.Start();

            foreach (SelectConditionPack _selectConditionPack in selectConditionPack.SelectConditionPacks)
            {
                var _expression = CreateExpressionFromSelectionPack(_selectConditionPack, parameter);
                _finalExpression = _expressionBuilder.Build(_finalExpression, _expression);
            }

            foreach (SelectCondition _selectCondition in selectConditionPack.SelectConditions)
            {
                var _expression = CreateExpressionFromCondition(parameter, _selectCondition);
                _finalExpression = _expressionBuilder.Build(_finalExpression, _expression);
            }

            if (selectConditionPack.Not)
            {
                _finalExpression = Expression.Not(_finalExpression);
            }

            return _finalExpression;
        }

        private void ConvertProperty(SelectCondition selectCondition, ref Expression member)
        {
            var _convert = _valueConverterStrategy.GetExpressionBuilder(selectCondition.Converter);
            if (_convert is NullConverter)
            {
                return;
            }

            member = member switch
            {
                MemberExpression _memberExpression => _convert.Convert(_memberExpression),
                ParameterExpression _parameterExpression => _convert.Convert(_parameterExpression),
                _ => throw new ArgumentException("Unexpected type of member", nameof(member))
            };
        }
    }
}
