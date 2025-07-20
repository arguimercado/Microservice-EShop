using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Catalog.Api.Commons.Extensions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var errorResponse = new
        {
            Message = "An unexpected error occurred.",
            Details = exception.Message,
            StackTrace = exception.StackTrace
        };


        var errorResponseJson = JsonSerializer.Serialize(errorResponse);


        await httpContext.Response.WriteAsync(errorResponseJson, cancellationToken);

        return true;
    }
}
