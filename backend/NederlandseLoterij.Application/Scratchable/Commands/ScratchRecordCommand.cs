using MediatR;
using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Scratchable.Commands;

/// <summary>
/// Command to create a scratch record for a user.
/// </summary>
public class ScratchRecordCommand : IRequest<ScratchableRecordDto>
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the scratchable item.
    /// </summary>
    public int Id { get; set; }
}