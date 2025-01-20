namespace NederlandseLoterij.Domain.Exceptions;

/// <summary>
/// Represents errors that occur during application validation.
/// </summary>
public class ValidationException(string message) : Exception(message)
{
}