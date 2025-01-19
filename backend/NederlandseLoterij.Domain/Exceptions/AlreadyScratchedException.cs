namespace NederlandseLoterij.Domain.Exceptions;

/// <summary>
/// Exception thrown when an attempt is made to scratch an already scratched item.
/// </summary>
public class AlreadyScratchedException(string message) : Exception(message)
{
}