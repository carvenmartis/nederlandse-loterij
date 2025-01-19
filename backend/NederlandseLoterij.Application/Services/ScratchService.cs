using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Domain.Entities;
using NederlandseLoterij.Domain.Exceptions;

namespace NederlandseLoterij.Application.Services;
/// <inheritdoc />

public class ScratchService(IScratchRepository repository) : IScratchService
{
    private readonly IScratchRepository _repository = repository;

    /// <inheritdoc />
    public async Task<IEnumerable<ScratchableArea>> GetScratchableAreasAsync(CancellationToken cancellationToken)
        => await _repository.GetAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<ScratchableArea> ScratchSquareAsync(int id, CancellationToken cancellationToken)
    {
        var area = await _repository.GetByIdAsync(id, cancellationToken);

        if (area == null)
            throw new KeyNotFoundException("Square not found.");

        if (area.IsScratched)
            throw new AlreadyScratchedException("This square is already scratched.");

        area.IsScratched = true;
        await _repository.UpdateAsync(area, cancellationToken);

        return area!;
    }
}