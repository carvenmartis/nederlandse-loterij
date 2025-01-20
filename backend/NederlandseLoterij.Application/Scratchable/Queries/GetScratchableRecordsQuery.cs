using MediatR;
using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Scratchable.Queries;

/// <summary>
/// Query to get a list of scratchable records.
/// </summary>
public class GetScratchableRecordsQuery : IRequest<List<ScratchableRecordDto>>
{ }