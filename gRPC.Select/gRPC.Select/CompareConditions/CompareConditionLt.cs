using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionLt : ICompareCondition
    {
        public BinaryExpression Build(Expression left, Expression right)
        {
            return Expression.LessThan(left, right);
        }
    }
}
