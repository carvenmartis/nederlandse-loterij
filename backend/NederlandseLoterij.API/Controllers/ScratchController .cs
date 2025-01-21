using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NederlandseLoterij.Application.Hubs;
using NederlandseLoterij.Application.Scratchable.Commands;
using NederlandseLoterij.Application.Scratchable.Queries;

namespace NederlandseLoterij.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScratchController(IMediator mediator, IHubContext<ScratchHub> hubContext) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IHubContext<ScratchHub> _hubContext = hubContext;

    [HttpGet]
    public async Task<IActionResult> GetScratchableAreas(CancellationToken cancellationToken = default)
        => Ok(await _mediator.Send(new GetAllRecordsQuery(), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> ScratchSquare([FromBody] ScratchRecordCommand scratchRecordCommand, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(scratchRecordCommand, cancellationToken);
        await _hubContext.Clients.All.SendAsync("ReceiveScratchUpdate", new object[] { result.Id, result.Prize });

        await _hubContext.Clients.All.SendAsync("SquareScratched", result.Id, cancellationToken);
        return Ok(result);
    }
}