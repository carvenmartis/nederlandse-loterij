namespace NederlandseLoterij.Application.GetGridState.Dtos;

/// <summary>
/// Represents the state of a grid item in the lottery application.
/// </summary>
public class GridStateDto
{
    /// <summary>
    /// Gets or sets the index of the grid item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the prize associated with the grid item.
    /// </summary>
    public string? Prize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the grid item has been scratched.
    /// </summary>
    public bool IsScratched { get; set; }
}