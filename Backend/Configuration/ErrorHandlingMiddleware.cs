using Backend.Core.Exceptions;

namespace Backend.API.Configuration;

//public class ErrorHandlingMiddleware
//{
//    private readonly RequestDelegate _next;

//    public ErrorHandlingMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        try
//        {
//            await _next(context);
//        }
//        catch (NotFoundException e)
//        {
//            context.Response.StatusCode = StatusCodes.Status404NotFound;
//            await context.Response.WriteAsJsonAsync(e.Message);
//        }
//        catch (BadRequestException e)
//        {
//            context.Response.StatusCode = StatusCodes.Status400BadRequest;
//            await context.Response.WriteAsJsonAsync(e.Message);
//        }
//        catch (Exception e)
//        {
//            await context.Response.WriteAsync(e.Message);
//            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        }
//    }
//}