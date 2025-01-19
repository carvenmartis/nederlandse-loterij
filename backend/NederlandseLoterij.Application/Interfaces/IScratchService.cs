using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Application.Interfaces;

/// <summary>
/// Provides methods to interact with scratchable areas in lottery tickets.
/// </summary>
public interface IScratchService
{
    /// <summary>
    /// Retrieves all scratchable areas asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of scratchable areas.</returns>
    Task<IEnumerable<ScratchableArea>> GetScratchableAreasAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Scratches a specific square asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the scratchable area to scratch.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the scratched area.</returns>
    Task<ScratchableArea> ScratchSquareAsync(int id, CancellationToken cancellationToken);
}