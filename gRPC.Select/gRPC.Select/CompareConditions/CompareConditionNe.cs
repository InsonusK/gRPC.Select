using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionNe : ICompareCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.NotEqual(left, right);
        }
    }
}
