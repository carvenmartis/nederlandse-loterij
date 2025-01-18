using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.Domain;

namespace NederlandseLoterij.Infrastructure;

/// <summary>
/// Represents the application database context for the Nederlandse Loterij.
/// This context manages the entity framework operations for the GridState entity.
/// </summary>
internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the collection of GridState entities.
    /// </summary>
    public DbSet<GridState> GridStates { get; set; }

    /// <summary>
    /// Configures the model for the GridState entity.
    /// </summary>
    /// <param name="modelBuilder">The model builder to configure the entity.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // GridState configuration
        modelBuilder.Entity<GridState>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary key
            entity.Property(e => e.Id)
                  .ValueGeneratedNever();

            entity.Property(e => e.Prize)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.IsScratched)
                  .IsRequired();
        });

        // Seed data for GridStates (optional)
        SeedGridStates(modelBuilder);
    }

    /// <summary>
    /// Seeds the initial data for the GridState entities.
    /// </summary>
    /// <param name="modelBuilder">The model builder to configure the entity data.</param>
    private static void SeedGridStates(ModelBuilder modelBuilder)
    {
        var gridData = new List<GridState>();

        for (int i = 0; i < 10000; i++)
        {
            var prize = i == 0 ? "€25,000" : i < 101 ? "Consolation Prize" : "No Prize";
            gridData.Add(new GridState
            {
                Id = i,
                Prize = prize,
                IsScratched = false
            }); ;
        }

        // Shuffle grid data for random placement of prizes
        var random = new Random();
        modelBuilder.Entity<GridState>()
            .HasData(gridData.OrderBy(_ => random.Next()));
    }
}