using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.LogicConditions
{
    public class LogicConditionOr : ILogicCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.OrElse(left, right);
        }

        public Expression Start()
        {
            return Expression.Constant(false);
        }
    }
}
