using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.LogicConditions
{
    public class LogicConditionAnd : ILogicCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }

        public Expression Start()
        {
            return Expression.Constant(true);
        }
    }
}
