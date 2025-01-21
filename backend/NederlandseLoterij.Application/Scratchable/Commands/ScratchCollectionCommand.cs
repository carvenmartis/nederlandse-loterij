using MediatR;
using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Scratchable.Commands;

/// <summary>
/// Command to create a scratch record for a user.
/// </summary>
public class ScratchCollectionCommand : IRequest<IEnumerable<ScratchableRecordDto>>
{
    /// <summary>
    /// Gets or sets the list of scratch record commands.
    /// </summary>
    public List<ScratchRecordCommand> Records { get; set; } = [];
}