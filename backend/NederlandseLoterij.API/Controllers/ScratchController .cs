using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NederlandseLoterij.Application.Hubs;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Models;

namespace NederlandseLoterij.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScratchController(IScratchService scratchService, IHubContext<ScratchHub> hubContext) : ControllerBase
{
    private readonly IScratchService _scratchService = scratchService;
    private readonly IHubContext<ScratchHub> _hubContext = hubContext;

    [HttpGet]
    public async Task<IActionResult> GetScratchableAreas(CancellationToken cancellationToken = default)
        => Ok(await _scratchService.GetScratchableAreasAsync(cancellationToken));

    [HttpPost]
    public async Task<IActionResult> ScratchSquare([FromBody] ScratchRequest scratchRequest, CancellationToken cancellationToken = default)
    {
        var result = await _scratchService.ScratchSquareAsync(scratchRequest.Id, cancellationToken);
        await _hubContext.Clients.All.SendAsync("ReceiveScratchUpdate", result.Id, result.Prize);
        return Ok(result);
    }
}