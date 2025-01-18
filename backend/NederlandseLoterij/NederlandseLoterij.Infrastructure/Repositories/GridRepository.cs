using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Domain;

namespace NederlandseLoterij.Infrastructure.Repositories;

/// <inheritdoc />
internal class GridRepository(AppDbContext dbContext) : IGridRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<IEnumerable<GridState>> GetGridAsync()
        => await _dbContext.GridStates.ToListAsync();

    /// <inheritdoc />
    public async Task<GridState?> GetSquareAsync(int id)
        => await _dbContext.GridStates.FirstOrDefaultAsync(g => g.Id == id);

    /// <inheritdoc />
    public async Task UpdateSquareAsync(GridState gridState)
    {
        _dbContext.GridStates.Update(gridState);
        await _dbContext.SaveChangesAsync();
    }
}