using NederlandseLoterij.Application.Scratchable.Dtos;

namespace NederlandseLoterij.Application.Interfaces;

/// <summary>
/// Interface for user repository to handle user-related data operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user data transfer object if found; otherwise, null.</returns>
    Task<UserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new user to the repository.
    /// </summary>
    /// <param name="user">The user data transfer object to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddUserAsync(UserDto user);

    /// <summary>
    /// Saves all changes made in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);
}