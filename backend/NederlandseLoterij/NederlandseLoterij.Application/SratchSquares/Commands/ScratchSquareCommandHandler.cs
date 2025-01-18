using MediatR;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.SratchSquares.Responses;

namespace NederlandseLoterij.Application.SratchSquares.Commands;

/// <summary>
/// Handler for processing scratch square commands.
/// </summary>
public class ScratchSquareCommandHandler(IGridRepository repository) : IRequestHandler<ScratchSquareCommand, ScratchSquareResponse>
{
    private readonly IGridRepository _repository = repository;

    /// <summary>
    /// Handles the scratch square command.
    /// </summary>
    /// <param name="command">The scratch square command request.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the response of the scratch square operation.</returns>
    public async Task<ScratchSquareResponse> Handle(ScratchSquareCommand command, CancellationToken cancellationToken)
    {
        var square = await _repository.GetSquareAsync(command.Index);

        if (square == null)
        {
            return new ScratchSquareResponse
            {
                Success = false,
                Message = "The square does not exist."
            };
        }

        if (square.IsScratched)
        {
            return new ScratchSquareResponse
            {
                Success = false,
                Message = "The square has already been scratched."
            };
        }

        square.IsScratched = true;
        await _repository.UpdateSquareAsync(square);

        return new ScratchSquareResponse
        {
            Success = true,
            Prize = square.Prize,
            Message = "The square has been scratched successfully."
        };
    }
}