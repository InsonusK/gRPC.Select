using System;
using System.Runtime.Serialization;

namespace gRPC.Select.Exceptions
{
    [Serializable]
    public class GRpcSelectException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public GRpcSelectException()
        {
        }

        public GRpcSelectException(string message) : base(message)
        {
        }

        public GRpcSelectException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GRpcSelectException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
