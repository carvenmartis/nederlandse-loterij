using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Infrastructure;

public interface IAppDbContext
{
    /// <summary>
    /// Gets or sets the DbSet for ScratchableArea entities.
    /// </summary>
    DbSet<ScratchableArea> ScratchableAreas { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for User entities.
    /// </summary>
    DbSet<User> Users { get; set; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}