using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Text.Json;

namespace NederlandseLoterij.API.Middelwares;

/// <summary>
/// Middleware to handle exceptions globally in the application.
/// Logs the exception and returns a standardized error response.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, ProblemDetailsFactory problemDetailsFactory)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unexpected error occurred");

        // Determine the status code
        var statusCode = exception switch
        {
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        // Create a ProblemDetails instance
        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode,
            title: "An error occurred while processing your request.",
            detail: exception.Message,
            instance: context.Request.Path
        );

        // Customize the response
        problemDetails.Extensions["traceId"] = context.TraceIdentifier; // Add trace ID for debugging

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        // Serialize ProblemDetails to JSON
        return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}