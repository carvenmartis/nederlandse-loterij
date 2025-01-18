namespace NederlandseLoterij.Application.SratchSquares.Responses;

/// <summary>
/// Represents the response for a scratch square operation.
/// </summary>
public class ScratchSquareResponse
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The prize won, if any.
    /// </summary>
    public string? Prize { get; set; }

    /// <summary>
    /// A message providing additional information about the operation.
    /// </summary>
    public required string Message { get; set; }
}