using MediatR;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Domain.Exceptions;

namespace NederlandseLoterij.Application.Scratchable.Commands;

/// <summary>
/// Handles the scratch record command.
/// </summary>
public class ScratchRecordCommandHandler(IUserRepository userRepository, IScratchableAreaRepository scratchableAreaRepository)
    : IRequestHandler<ScratchRecordCommand, ScratchableRecordDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IScratchableAreaRepository _scratchableAreaRepository = scratchableAreaRepository;

    /// <summary>
    /// Handles the scratch record command.
    /// </summary>
    /// <param name="request">The scratch record command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The scratchable record DTO.</returns>
    /// <exception cref="ValidationException">Thrown when the user has already scratched a record.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the record is not found.</exception>
    public async Task<ScratchableRecordDto> Handle(ScratchRecordCommand request, CancellationToken cancellationToken)
    {
        var scratchableArea = await _scratchableAreaRepository.GetRecordByIdAsync(request.Id, cancellationToken);

        if (scratchableArea == null)
        {
            throw new KeyNotFoundException("Record not found.");
        }

        var user = await _userRepository.GetUserAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            user = new UserDto { Id = request.UserId, HasScratched = true };
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }
        else if (user.HasScratched)
        {
            throw new ValidationException("User has already scratched a record.");
        }

        user.HasScratched = true;
        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        scratchableArea.IsScratched = true;
        await _scratchableAreaRepository.AddScratchableAreaAsync(scratchableArea, cancellationToken);
        await _scratchableAreaRepository.SaveChangesAsync(cancellationToken);

        return scratchableArea;
    }
}