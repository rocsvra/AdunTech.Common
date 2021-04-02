using System;
using System.Runtime.Serialization;

namespace AdunTech.ExceptionDetail
{
    [Serializable]
    public class BadRequestException : HttpBaseException
    {
        public BadRequestException(string traceId, string code, params string[] args)
            : base(traceId, code, args)
        {
        }

        public BadRequestException(string traceId, string code, Exception innerException, params string[] args)
            : base(traceId, code, innerException, args)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
