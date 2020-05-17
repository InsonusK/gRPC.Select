using System;
using System.Runtime.Serialization;

namespace gRPC.Select.Exceptions
{
    [Serializable]
    public class ConverterException : GRpcSelectException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConverterException()
        {
        }

        public ConverterException(string message) : base(message)
        {
        }

        public ConverterException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConverterException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
