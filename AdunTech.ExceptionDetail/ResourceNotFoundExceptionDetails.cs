using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace AdunTech.ExceptionDetail
{
    public class ResourceNotFoundExceptionDetails : StatusCodeProblemDetails
    {
        public ResourceNotFoundExceptionDetails(string errUrl, ResourceNotFoundException ex)
            : base(StatusCodes.Status404NotFound)
        {
            Type = errUrl + (ex.Code ?? ex.GetType().Name);
            Title = ex.Message;
            Detail = ex.Detail;
            TraceId = ex.TraceId;
        }

        public string TraceId { get; }
    }
}
