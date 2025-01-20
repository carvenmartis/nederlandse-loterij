namespace NederlandseLoterij.Application.Scratchable.Dtos;

/// <summary>
/// Represents a record of a scratchable item in the Nederlandse Loterij application.
/// </summary>
public class ScratchableRecordDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the scratchable item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item has been scratched.
    /// </summary>
    public bool IsScratched { get; set; }

    /// <summary>
    /// Gets or sets the prize associated with the scratchable item.
    /// </summary>
    public string Prize { get; set; } = string.Empty;
}