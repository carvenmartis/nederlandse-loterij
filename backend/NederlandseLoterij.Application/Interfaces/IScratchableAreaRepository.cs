using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Interfaces;

/// <summary>
/// Interface for managing scratchable records.
/// </summary>
public interface IScratchableAreaRepository
{
    /// <summary>
    /// Retrieves all scratchable records.
    /// </summary>
    /// <returns>A list of scratchable record DTOs.</returns>
    Task<List<ScratchableRecordDto>> GetAllRecordsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all scratchable records.
    /// </summary>
    /// <returns>A list of scratchable record DTOs.</returns>
    Task<List<ScratchableRecordDto>> GetScratchableRecordsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a scratchable record by its ID.
    /// </summary>
    /// <param name="recordId">The ID of the record to retrieve.</param>
    /// <returns>The scratchable record DTO if found; otherwise, null.</returns>
    Task<ScratchableRecordDto> GetRecordByIdAsync(int recordId, CancellationToken cancellationToken);

    /// <summary>
    /// Saves changes to the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task AddScratchableAreaAsync(ScratchableRecordDto scratchableRecordDto, CancellationToken cancellationToken);
}