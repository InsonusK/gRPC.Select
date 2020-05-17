using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionLe : ICompareCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }
    }
}
