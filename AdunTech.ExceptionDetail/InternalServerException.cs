using System;
using System.Runtime.Serialization;

namespace AdunTech.ExceptionDetail
{
    [Serializable]
    public class InternalServerException : HttpBaseException
    {
        private readonly Exception _innerException;

        public InternalServerException(string traceId, Exception innerException)
            : base(traceId, "InternalServerError", innerException)
        {
            _innerException = innerException;
        }

        protected InternalServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public override string Message => Code;

        public override string Detail => _innerException.Message;
    }
}
