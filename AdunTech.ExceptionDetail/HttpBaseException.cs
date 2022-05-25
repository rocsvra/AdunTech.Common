using System;
using System.Runtime.Serialization;

namespace AdunTech.ExceptionDetail
{
    /// <summary>
    /// http异常基类
    /// </summary>
    public abstract class HttpBaseException : Exception
    {
        private readonly string[] _args;

        protected HttpBaseException(string traceId, string code, params string[] args)
            : base(code)
        {
            _args = args;
            TraceId = traceId;
            Code = code;
        }

        protected HttpBaseException(string traceId, string code, Exception innerException, params string[] args)
            : base(code, innerException)
        {
            _args = args;
            TraceId = traceId;
            Code = code;
        }

        protected HttpBaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 请求上下文的traceId
        /// </summary>
        public string TraceId { get; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public override string Message => string.Format("Code:{0}", Code);

        /// <summary>
        /// 错误明细
        /// </summary>  
        public virtual string Detail
        {
            get
            {
                string argsMsg = _args.Length > 0 ? string.Join("|", _args) : string.Empty;
                if (string.IsNullOrEmpty(argsMsg))
                {
                    return string.Format("Detail:{0}", Code);
                }
                return string.Format("{0}_Detail:{1};", Code, argsMsg);
            }
        }
    }
}
