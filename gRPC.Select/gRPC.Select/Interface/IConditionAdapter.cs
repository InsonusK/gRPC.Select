using Google.Protobuf;
using GRPC.Selector;

namespace gRPC.Select.Adapter
{
    /// <summary>
    /// Adapter to project message
    /// </summary>
    /// <typeparam name="TConditionMessage">Condition message type</typeparam>
    public interface IConditionAdapter<in TConditionMessage>
    {
        /// <summary>
        /// Convert to project type
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        public SelectRequest Convert(TConditionMessage message);
    }
}
