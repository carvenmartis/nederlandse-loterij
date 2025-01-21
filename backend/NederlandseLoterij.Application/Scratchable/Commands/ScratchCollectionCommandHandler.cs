using MediatR;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Domain.Exceptions;

namespace NederlandseLoterij.Application.Scratchable.Commands;

/// <summary>
/// Handles the scratch record command.
/// </summary>
public class ScratchCollectionCommandHandler(IUserRepository userRepository, IScratchableAreaRepository scratchableAreaRepository)
    : IRequestHandler<ScratchCollectionCommand, IEnumerable<ScratchableRecordDto>>
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
    public async Task<IEnumerable<ScratchableRecordDto>> Handle(ScratchCollectionCommand request, CancellationToken cancellationToken)
    {
        var result = new List<ScratchableRecordDto>();

        foreach (var record in request.Records)
        {
            var scratchableArea = await _scratchableAreaRepository.GetRecordByIdAsync(record.Id, cancellationToken);
            if (scratchableArea == null)
            {
                throw new KeyNotFoundException($"Record with ID {record.Id} not found.");
            }

            var user = await _userRepository.GetUserAsync(record.UserId, cancellationToken);
            if (user == null)
            {
                user = new UserDto { Id = record.UserId, HasScratched = true };
                await _userRepository.AddUserAsync(user);
            }
            else if (user.HasScratched)
            {
                throw new ValidationException($"User with ID {record.UserId} has already scratched a record.");
            }

            user.HasScratched = true;
            scratchableArea.IsScratched = true;

            await _userRepository.AddUserAsync(user);
            await _scratchableAreaRepository.AddScratchableAreaAsync(scratchableArea, cancellationToken);

            result.Add(scratchableArea);
        }

        await _userRepository.SaveChangesAsync(cancellationToken);
        await _scratchableAreaRepository.SaveChangesAsync(cancellationToken);

        return result;
    }
}