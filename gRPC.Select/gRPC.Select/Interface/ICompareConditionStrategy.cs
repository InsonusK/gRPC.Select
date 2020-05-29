using GRPC.Selector.Enum;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Repository of available compare conditions
    /// </summary>
    public interface ICompareConditionStrategy
    {
        /// <summary>
        /// Get compare condition expression builder by condition name
        /// </summary>
        /// <param name="condition">Condition name</param>
        /// <returns></returns>
        ICompareCondition GetExpressionBuilder(CompareCondition condition);
    }
}
