using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionGt : ICompareCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
    }
}
