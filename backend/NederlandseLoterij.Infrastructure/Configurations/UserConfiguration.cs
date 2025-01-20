using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NederlandseLoterij.Domain.Entities;

namespace NederlandseLoterij.Infrastructure.Configurations;

/// <summary>
/// Configures the User entity and its relationships.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the User entity.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the User entity.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasOne(u => u.ScratchableArea)
            .WithOne(x => x.User)
            .HasForeignKey<ScratchableArea>(p => p.UserId);
    }
}