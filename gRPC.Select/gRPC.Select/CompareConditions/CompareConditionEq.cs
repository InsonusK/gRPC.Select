using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionEq : ICompareCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }
    }
}
