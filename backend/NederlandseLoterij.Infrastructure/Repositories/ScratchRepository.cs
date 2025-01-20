using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Infrastructure.Repositories;

/// <inheritdoc />
public class ScratchRepository(AppDbContext context) : IScratchRepository
{
    private readonly AppDbContext _context = context;

    /// <inheritdoc />
    public async Task<IEnumerable<ScratchableArea>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.ScratchableAreas.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<ScratchableArea>> GetAllScratchableAreas(CancellationToken cancellationToken)
         => await _context.ScratchableAreas.Where(x => x.IsScratched == false).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<ScratchableArea?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context.ScratchableAreas.FindAsync(id, cancellationToken);

    /// <inheritdoc />
    public async Task UpdateAsync(ScratchableArea area, CancellationToken cancellationToken)
    {
        _context.ScratchableAreas.Update(area);
        await _context.SaveChangesAsync(cancellationToken);
    }
}