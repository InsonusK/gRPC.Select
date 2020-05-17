using System;
using gRPC.Select.Interface;

namespace gRPC.Select.CompareConditions
{
    public class CompareConditionStrategy : ICompareConditionStrategy
    {
        public ICompareCondition GetExpressionBuilder(GRPC.Selector.CompareCondition condition)
        {
            return condition switch
            {
                GRPC.Selector.CompareCondition.Eq => new CompareConditionEq(),
                GRPC.Selector.CompareCondition.Ne => new CompareConditionNe(),
                GRPC.Selector.CompareCondition.Gt => new CompareConditionGt(),
                GRPC.Selector.CompareCondition.Lt => new CompareConditionLt(),
                GRPC.Selector.CompareCondition.Ge => new CompareConditionGe(),
                GRPC.Selector.CompareCondition.Le => new CompareConditionLe(),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }
    }
}
