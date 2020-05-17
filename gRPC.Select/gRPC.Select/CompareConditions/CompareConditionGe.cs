using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionGe : ICompareCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }
    }
}
