using MediatR;
using NederlandseLoterij.Application.GetGridState.Dtos;

namespace NederlandseLoterij.Application.GetGridState.Queries;

public class GetGridStateQuery : IRequest<IEnumerable<GridStateDto>>
{
    public int Id { get; set; }
}