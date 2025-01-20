using MediatR;
using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Scratchable.Queries;

/// <summary>
/// Query to get all scratchable records.
/// </summary>
public class GetAllRecordsQuery : IRequest<IEnumerable<ScratchableRecordDto>>
{
}