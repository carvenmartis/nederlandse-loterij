using MediatR;
using Microsoft.Extensions.Logging;

namespace NederlandseLoterij.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            // Proceed to the next behavior or handler
            return await next();
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unhandled exception occurred during the processing of {Request}", typeof(TRequest).Name);

            // Rethrow or transform the exception (optional)
            throw new ApplicationException($"An error occurred while processing the request: {typeof(TRequest).Name}", ex);
        }
    }
}