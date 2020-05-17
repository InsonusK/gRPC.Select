using System;
using gRPC.Select.Interface;

namespace gRPC.Select.LogicConditions
{
    public class LogicConditionStrategy : ILogicConditionStrategy
    {
        public ILogicCondition GetExpressionBuilder(GRPC.Selector.LogicCondition condition)
        {
            return condition switch
            {
                GRPC.Selector.LogicCondition.And => new LogicConditionAnd(),
                GRPC.Selector.LogicCondition.Or => new LogicConditionOr(),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }
    }
}
