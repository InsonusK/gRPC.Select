using Google.Protobuf;
using gRPC.Select.Interface;
using GRPC.Selector;

namespace gRPC.Select.Adapter
{
    /// <summary>
    /// Adapter for classes made from select.proto
    /// </summary>
    /// <typeparam name="TMessage">Proto message type</typeparam>
    public class BaseProtoAdapter<TMessage> : IConditionAdapter<TMessage>
    {
        public SelectRequest Convert(TMessage message)
        {
            SelectRequest _selectRequest = new SelectRequest();
            _selectRequest.MergeFrom(new CodedInputStream(((IMessage)message).ToByteArray()));
            return _selectRequest;
        }
    }
}
