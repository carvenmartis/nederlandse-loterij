using Microsoft.AspNetCore.Mvc.Infrastructure;
using NederlandseLoterij.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace NederlandseLoterij.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
{
    private readonly RequestDelegate _next = next;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            AlreadyScratchedException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode: (int)statusCode,
            title: nameof(statusCode),
            detail: exception.Message,
            instance: context.Request.Path
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}