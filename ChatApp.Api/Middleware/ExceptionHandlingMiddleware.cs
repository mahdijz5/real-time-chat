using System.Net;
using System.Text.Json;
using ChatApp.Application.Exceptions;
using Microsoft.Extensions.Logging;
namespace ChatApp.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred during the request.");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode;
        object response;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    message = "One or more validation errors occurred.",
                    errors = validationException.Errors
                };
                break;

            case ConflictException:
                statusCode = HttpStatusCode.Conflict;
                response = new
                {
                    message = exception.Message,
                    errors = new Dictionary<string, string[]>()
                };
                break;

            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                response = new
                {
                    message = exception.Message,
                    errors = new Dictionary<string, string[]>()
                };
                break;

            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    message = exception.Message,
                    errors = new Dictionary<string, string[]>()
                };
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                response = new
                {
                    message = exception.Message,
                    errors = new Dictionary<string, string[]>()
                };
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                response = new
                {
                    message = "An unexpected error occurred.",
                    errors = new Dictionary<string, string[]>()
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}