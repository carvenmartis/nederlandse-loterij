using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Infrastructure.Entities;

namespace NederlandseLoterij.Infrastructure;

/// <summary>
/// Represents the application's database context.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    /// <summary>
    /// Gets or sets the DbSet for ScratchableArea entities.
    /// </summary>
    public DbSet<ScratchableArea> ScratchableAreas { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for User entities.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Configures the model and seeds initial data.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed 10,000 scratchable areas
        var areas = new List<ScratchableArea>
        {
            new() { Id = Guid.NewGuid(), Prize = "€25,000" }
        };

        areas.AddRange(
            Enumerable.Range(2, 100).Select(id => new ScratchableArea
            {
                Id = Guid.NewGuid(),
                Prize = "Consolation Prize"
            })
        );

        areas.AddRange(
            Enumerable.Range(102, 9899).Select(id => new ScratchableArea
            {
                Id = Guid.NewGuid()
            })
        );

        var shuffledAreas = areas.OrderBy(_ => Guid.NewGuid()).ToList();

        for (int i = 0; i < shuffledAreas.Count; i++)
        {
            shuffledAreas[i].Index = i + 1;
        }

        modelBuilder.Entity<ScratchableArea>().HasData(shuffledAreas);
    }
}