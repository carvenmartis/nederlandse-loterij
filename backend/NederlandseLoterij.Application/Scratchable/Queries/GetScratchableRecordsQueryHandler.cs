using MediatR;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Scratchable.Queries;

/// <summary>
/// Handles the query to get scratchable records.
/// </summary>
public class GetScratchableRecordsQueryHandler(IScratchableAreaRepository scratchableAreaRepository)
    : IRequestHandler<GetScratchableRecordsQuery, List<ScratchableRecordDto>>
{
    private readonly IScratchableAreaRepository _scratchableAreaRepository = scratchableAreaRepository;

    /// <summary>
    /// Handles the request to get scratchable records.
    /// </summary>
    /// <param name="request">The request to get scratchable records.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of scratchable record DTOs.</returns>
    public async Task<List<ScratchableRecordDto>> Handle(GetScratchableRecordsQuery request, CancellationToken cancellationToken)
    {
        var records = await _scratchableAreaRepository.GetScratchableRecordsAsync(cancellationToken);
        return records.Select(r => new ScratchableRecordDto
        {
            Id = r.Id,
            IsScratched = r.IsScratched,
            Prize = r.Prize
        }).ToList();
    }
}