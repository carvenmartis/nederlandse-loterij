using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NederlandseLoterij.Domain;

namespace NederlandseLoterij.Infrastructure.Configurations;

public class GridStateConfiguration : IEntityTypeConfiguration<GridState>
{
    public void Configure(EntityTypeBuilder<GridState> builder)
    {
        builder.HasKey(g => g.Id); // Primary Key

        builder.Property(g => g.Prize)
               .IsRequired()
               .HasMaxLength(100); // Max length for prize string

        builder.Property(g => g.IsScratched)
               .IsRequired(); // Default: false
    }
}