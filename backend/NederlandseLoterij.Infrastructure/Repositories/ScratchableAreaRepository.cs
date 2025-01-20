using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Infrastructure.Repositories;

/// <inheritdoc />
public class ScratchableAreaRepository(IAppDbContext dbContext) : IScratchableAreaRepository
{
    private readonly IAppDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<List<ScratchableRecordDto>> GetAllRecordsAsync(CancellationToken cancellationToken)
      => await _dbContext.ScratchableAreas
            .Select(r => new ScratchableRecordDto
            {
                Id = r.Id,
                IsScratched = r.IsScratched,
                Prize = r.Prize
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<List<ScratchableRecordDto>> GetScratchableRecordsAsync(CancellationToken cancellationToken)
        => await _dbContext.ScratchableAreas
            .Where(r => !r.IsScratched)
            .Select(r => new ScratchableRecordDto
            {
                Id = r.Id,
                IsScratched = r.IsScratched,
                Prize = r.Prize
            })
            .ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<ScratchableRecordDto?> GetRecordByIdAsync(int recordId, CancellationToken cancellationToken)
    {
        var record = await _dbContext.ScratchableAreas.FirstOrDefaultAsync(r => r.Id == recordId, cancellationToken);
        if (record == null) return null;

        return new ScratchableRecordDto
        {
            Id = record.Id,
            IsScratched = record.IsScratched,
            Prize = record.Prize
        };
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
       => await _dbContext.SaveChangesAsync(cancellationToken);

    /// <inheritdoc />
    public async Task AddScratchableAreaAsync(ScratchableRecordDto scratchableRecordDto, CancellationToken cancellationToken)
    {
        var record = await _dbContext.ScratchableAreas.FindAsync(scratchableRecordDto.Id, cancellationToken);

        if (record == null)
        {
            record = new ScratchableArea
            {
                Id = scratchableRecordDto.Id,
            };

            await _dbContext.ScratchableAreas.AddAsync(record);
        }

        record.IsScratched = scratchableRecordDto.IsScratched;
        record.Prize = scratchableRecordDto.Prize;
    }
}