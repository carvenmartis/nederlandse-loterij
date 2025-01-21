namespace NederlandseLoterij.Infrastructure.Entities;

/// <summary>
/// Represents a scratchable area in a lottery ticket.
/// </summary>
public class ScratchableArea
{
    /// <summary>
    /// Gets or sets the unique identifier for the scratchable area.
    /// </summary>
    public Guid Id { get; set; }

    public int Index { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the area has been scratched.
    /// </summary>
    public bool IsScratched { get; set; }

    /// <summary>
    /// Gets or sets the prize associated with the scratchable area.
    /// </summary>
    public string Prize { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the scratchable area.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the scratchable area.
    /// </summary>
    public User? User { get; set; }
}