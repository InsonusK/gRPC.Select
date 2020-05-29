using System;
using gRPC.Select.Interface;
using GRPC.Selector.Enum;

namespace gRPC.Select.LogicConditions
{
    public class LogicConditionStrategy : ILogicConditionStrategy
    {
        public ILogicCondition GetExpressionBuilder(LogicCondition condition)
        {
            return condition switch
            {
                LogicCondition.And => new LogicConditionAnd(),
                LogicCondition.Or => new LogicConditionOr(),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }
    }
}
