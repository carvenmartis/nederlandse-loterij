using MediatR;
using Microsoft.AspNetCore.Mvc;
using NederlandseLoterij.API.Models;
using NederlandseLoterij.Application.GetGridState.Queries;

namespace NederlandseLoterij.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GridController : ControllerBase
{
    private readonly IMediator _mediator;

    public GridController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves the current state of the grid.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetGridState()
    {
        var result = await _mediator.Send(new GetGridStateQuery());
        return Ok(result);
    }

    /// <summary>
    /// Scratches a specific square on the grid.
    /// </summary>
    /// <param name="request">Details of the square to scratch.</param>
    [HttpPost("scratch")]
    public async Task<IActionResult> ScratchSquare([FromBody] ScratchRequest request)
    {
        if (request == null || request.Id < 0)
        {
            return BadRequest(new { Message = "Invalid request. Index must be a positive number." });
        }

        var result = await _mediator.Send(new ScratchSquareCommand { Index = request.Id });

        if (!result.Success)
        {
            return BadRequest(new ScratchResponse
            {
                Success = false,
                Message = result.Message
            });
        }

        return Ok(new ScratchResponse
        {
            Success = true,
            Prize = result.Prize,
            Message = result.Message
        });
    }
}