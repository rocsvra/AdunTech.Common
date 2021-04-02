using System;
using System.Runtime.Serialization;

namespace AdunTech.ExceptionDetail
{
    [Serializable]
    public class ResourceNotFoundException : HttpBaseException
    {
        public ResourceNotFoundException(string traceId, string code, params string[] args)
           : base(traceId, code, args)
        {
        }

        public ResourceNotFoundException(string traceId, string code, Exception innerException, params string[] args)
            : base(traceId, code, innerException, args)
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
