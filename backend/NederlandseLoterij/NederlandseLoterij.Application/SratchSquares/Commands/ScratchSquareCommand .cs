using MediatR;
using NederlandseLoterij.Application.SratchSquares.Responses;

/// <summary>
/// Command to scratch a square in the lottery game.
/// </summary>
public class ScratchSquareCommand : IRequest<ScratchSquareResponse>
{
    /// <summary>
    /// The index of the square to be scratched.
    /// </summary>
    public int Index { get; set; }
}