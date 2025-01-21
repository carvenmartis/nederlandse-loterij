namespace NederlandseLoterij.Infrastructure.Entities;

/// <summary>
/// Represents a user in the Nederlandse Loterij system.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has scratched the scratchable area.
    /// </summary>
    public bool HasScratched { get; set; }

    /// <summary>
    /// Gets or sets the scratchable area associated with the user.
    /// </summary>
    public ScratchableArea? ScratchableArea { get; set; }
}