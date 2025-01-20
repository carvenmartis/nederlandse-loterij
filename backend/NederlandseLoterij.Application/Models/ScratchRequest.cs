namespace NederlandseLoterij.Application.Models;

/// <summary>
/// Represents a request to scratch a lottery ticket.
/// </summary>
public class ScratchRequest
{
    /// <summary>
    /// Gets or sets the identifier of the scratch request.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user making the scratch request.
    /// </summary>
    public Guid UserId { get; set; }
}