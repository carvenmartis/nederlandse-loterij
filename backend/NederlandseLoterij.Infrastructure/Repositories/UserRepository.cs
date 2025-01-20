using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Infrastructure.Repositories;

public class UserRepository(IAppDbContext dbContext) : IUserRepository
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task<UserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users
            .Include(u => u.ScratchableArea)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (existingUser == null)
            return null;

        return new UserDto()
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

    public async Task AddUserAsync(UserDto userDto)
    {
        var user = await _dbContext.Users.FindAsync(userDto.Id);
        if (user == null)
        {
            user = new User
            {
                Id = userDto.Id,
            };
            await _dbContext.Users.AddAsync(user);
        }

        user.HasScratched = userDto.HasScratched;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}