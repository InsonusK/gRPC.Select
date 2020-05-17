using System;
using System.Runtime.Serialization;

namespace gRPC.Select.Exceptions
{
    [Serializable]
    public class ConditionException : GRpcSelectException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConditionException()
        {
        }

        public ConditionException(string message) : base(message)
        {
        }

        public ConditionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConditionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
