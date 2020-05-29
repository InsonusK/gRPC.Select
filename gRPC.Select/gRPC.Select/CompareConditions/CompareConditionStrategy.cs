using System;
using gRPC.Select.Interface;
using GRPC.Selector.Enum;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionStrategy : ICompareConditionStrategy
    {
        public ICompareCondition GetExpressionBuilder(CompareCondition condition)
        {
            return condition switch
            {
                CompareCondition.Eq => new CompareConditionEq(),
                CompareCondition.Ne => new CompareConditionNe(),
                CompareCondition.Gt => new CompareConditionGt(),
                CompareCondition.Lt => new CompareConditionLt(),
                CompareCondition.Ge => new CompareConditionGe(),
                CompareCondition.Le => new CompareConditionLe(),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }
    }
}
