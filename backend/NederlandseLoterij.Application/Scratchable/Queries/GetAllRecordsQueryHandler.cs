using MediatR;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Scratchable.Queries;

/// <summary>
/// Handles the query to get all scratchable records.
/// </summary>
/// <param name="scratchableAreaRepository">The repository for scratchable areas.</param>
public class GetAllRecordsQueryHandler(IScratchableAreaRepository scratchableAreaRepository)
    : IRequestHandler<GetAllRecordsQuery, IEnumerable<ScratchableRecordDto>>
{
    /// <summary>
    /// Handles the request to get all scratchable records.
    /// </summary>
    /// <param name="request">The request to get all scratchable records.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of scratchable record DTOs.</returns>
    public async Task<IEnumerable<ScratchableRecordDto>> Handle(GetAllRecordsQuery request, CancellationToken cancellationToken)
    {
        var records = await scratchableAreaRepository.GetAllRecordsAsync(cancellationToken);
        return records.Select(r => new ScratchableRecordDto
        {
            Id = r.Id,
            IsScratched = r.IsScratched,
            Prize = r.Prize
        }).ToList();
    }
}