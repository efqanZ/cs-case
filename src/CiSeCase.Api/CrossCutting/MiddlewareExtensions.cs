using Microsoft.AspNetCore.Builder;

namespace CiSeCase.Api.CrossCutting
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}