using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Application.Interfaces;

/// <summary>
/// Interface for repository operations related to ScratchableArea entities.
/// </summary>
public interface IScratchRepository
{
    /// <summary>
    /// Gets all scratchable areas asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of scratchable areas.</returns>
    Task<IEnumerable<ScratchableArea>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets a scratchable area by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the scratchable area.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the scratchable area if found; otherwise, null.</returns>
    Task<ScratchableArea?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a scratchable area asynchronously.
    /// </summary>
    /// <param name="area">The scratchable area to update.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(ScratchableArea area, CancellationToken cancellationToken);
}