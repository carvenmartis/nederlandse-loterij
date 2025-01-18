using MediatR;
using NederlandseLoterij.Application.GetGridState.Dtos;
using NederlandseLoterij.Application.Interfaces;

namespace NederlandseLoterij.Application.GetGridState.Queries;

/// <summary>
/// Handles the retrieval of grid state information.
/// </summary>
public class GetGridStateQueryHandler(IGridRepository repository) : IRequestHandler<GetGridStateQuery, IEnumerable<GridStateDto>>
{
    private readonly IGridRepository _repository = repository;

    /// <summary>
    /// Handles the incoming request to get the grid state.
    /// </summary>
    /// <param name="request">The request containing the grid ID.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the grid state data.</returns>
    public async Task<IEnumerable<GridStateDto>> Handle(GetGridStateQuery request, CancellationToken cancellationToken)
    {
        var grid = await _repository.GetGridAsync();

        return grid.Select(g => new GridStateDto
        {
            Id = g.Id,
            Prize = g.IsScratched ? g.Prize : null,
            IsScratched = g.IsScratched
        });
    }
}