namespace NederlandseLoterij.Domain;

/// <summary>
/// Represents the state of a grid in the lottery system.
/// </summary>
public class GridState
{
    /// <summary>
    /// Gets or sets the unique identifier for the grid state.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the prize associated with the grid state.
    /// </summary>
    public required string Prize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the grid has been scratched.
    /// </summary>
    public bool IsScratched { get; set; }
}