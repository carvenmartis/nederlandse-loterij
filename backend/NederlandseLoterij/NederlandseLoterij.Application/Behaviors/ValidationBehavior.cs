using FluentValidation;
using MediatR;

namespace NederlandseLoterij.Application.Behaviors;

/// <summary>
/// A behavior that validates requests using a collection of validators.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    /// <summary>
    /// Handles the validation of the request before proceeding to the next handler.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <param name="next">The delegate to invoke the next handler.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The response from the next handler.</returns>
    /// <exception cref="ValidationException">Thrown when validation fails.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}