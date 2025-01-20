namespace NederlandseLoterij.Application.Scratchable.Dtos;

/// <summary>
/// Represents a user with a scratchable record.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has scratched.
    /// </summary>
    public bool HasScratched { get; set; }

    /// <summary>
    /// Gets or sets the scratchable area associated with the user.
    /// </summary>
    public ScratchableRecordDto? ScratchableArea { get; set; }
}