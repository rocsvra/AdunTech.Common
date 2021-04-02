using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace AdunTech.ExceptionDetail
{
    public class InternalServerExceptionDetails : StatusCodeProblemDetails
    {
        public InternalServerExceptionDetails(string errUrl, InternalServerException ex)
            : base(StatusCodes.Status500InternalServerError)
        {
            Type = errUrl + (ex.Code ?? ex.GetType().Name);
            Title = ex.Message;
            Detail = ex.Detail;
            TraceId = ex.TraceId;
        }

        public string TraceId { get; }
    }
}
