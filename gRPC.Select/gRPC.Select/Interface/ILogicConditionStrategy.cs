using GRPC.Selector;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Repository of available logic conditions
    /// </summary>
    public interface ILogicConditionStrategy
    {
        /// <summary>
        /// Get logic condition expression builder by condition name
        /// </summary>
        /// <param name="condition">Condition name</param>
        /// <returns></returns>
        ILogicCondition GetExpressionBuilder(LogicCondition condition);
    }
}
