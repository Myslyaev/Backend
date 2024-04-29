using Backend.Core.Exceptions;
using Serilog;
using System.Net;

namespace Backend.API.Configuration;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger =Log.ForContext<ExceptionMiddleware>();
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.Error($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware."
        }.ToString());
    }
}
