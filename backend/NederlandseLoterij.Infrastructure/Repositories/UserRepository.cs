using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Infrastructure.Entities;

namespace NederlandseLoterij.Infrastructure.Repositories;

/// <inheritdoc />
public class UserRepository(IAppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly IAppDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<UserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users
            .Include(u => u.ScratchableArea)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (existingUser == null)
            return null;

        return new UserDto
        {
            Id = existingUser.Id,
            HasScratched = existingUser.HasScratched,
            ScratchableArea = existingUser.ScratchableArea == null ? null : new ScratchableRecordDto
            {
                Id = existingUser.ScratchableArea.Id,
                IsScratched = existingUser.ScratchableArea.IsScratched,
                Prize = existingUser.ScratchableArea.Prize
            }
        };
    }

    /// <inheritdoc />
    public async Task AddUserAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(userDto.Id, cancellationToken);
        if (user == null)
        {
            user = new User
            {
                Id = userDto.Id,
            };

            await AddAsync(user);
        }

        user.HasScratched = userDto.HasScratched;
    }

    /// <inheritdoc />
    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
}