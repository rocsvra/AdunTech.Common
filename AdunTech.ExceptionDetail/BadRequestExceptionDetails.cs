using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace AdunTech.ExceptionDetail
{
    public class BadRequestExceptionDetails : StatusCodeProblemDetails
    {
        public BadRequestExceptionDetails(string errUrl, BadRequestException ex)
            : base(StatusCodes.Status400BadRequest)
        {
            Type = errUrl + (ex.Code ?? ex.GetType().Name);
            Title = ex.Message;
            Detail = ex.Detail;
            TraceId = ex.TraceId;
        }
        public string TraceId { get; }
    }
}
