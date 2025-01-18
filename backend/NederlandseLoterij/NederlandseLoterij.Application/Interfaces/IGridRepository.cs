using NederlandseLoterij.Domain;

namespace NederlandseLoterij.Application.Interfaces;

/// <summary>
/// Interface for grid repository operations, providing methods to retrieve and update grid states.
/// </summary>
public interface IGridRepository
{
    /// <summary>
    /// Asynchronously retrieves the current grid state.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing an enumerable collection of GridState.</returns>
    Task<IEnumerable<GridState>> GetGridAsync();

    /// <summary>
    /// Asynchronously retrieves a specific square in the grid by its index.
    /// </summary>
    /// <param name="index">The index of the square to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the GridState of the specified square, or null if not found.</returns>
    Task<GridState?> GetSquareAsync(int index);

    /// <summary>
    /// Asynchronously updates the state of a specific square in the grid.
    /// </summary>
    /// <param name="gridState">The updated GridState to apply to the square.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateSquareAsync(GridState gridState);
}