using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Infrastructure.Entities;

namespace NederlandseLoterij.Infrastructure.Repositories;

/// <inheritdoc />
public class ScratchableAreaRepository(IAppDbContext dbContext)
    : Repository<ScratchableArea>(dbContext), IScratchableAreaRepository
{
    private readonly IAppDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<IEnumerable<ScratchableRecordDto>> GetAllRecordsAsync(CancellationToken cancellationToken)
    {
        var records = await GetAllAsync(cancellationToken);

        return records.Select(r => new ScratchableRecordDto
        {
            Id = r.Id,
            IsScratched = r.IsScratched,
            Prize = r.Prize
        });
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ScratchableRecordDto>> GetScratchableRecordsAsync(CancellationToken cancellationToken)
    {
        var records = await FindAsync(r => !r.IsScratched, cancellationToken);

        return records
            .Select(r => new ScratchableRecordDto
            {
                Id = r.Id,
                IsScratched = r.IsScratched,
                Prize = r.Prize
            });
    }

    /// <inheritdoc />
    public async Task<ScratchableRecordDto> GetRecordByIdAsync(Guid recordId, CancellationToken cancellationToken)
    {
        var record = await GetByIdAsync(recordId, cancellationToken);
        if (record == null)
            throw new KeyNotFoundException($"Record with ID {recordId} not found.");

        return new ScratchableRecordDto
        {
            Id = record.Id,
            IsScratched = record.IsScratched,
            Prize = record.Prize
        };
    }

    /// <inheritdoc />
    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
       => await base.SaveChangesAsync(cancellationToken);

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