using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NederlandseLoterij.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Provide the connection string
        optionsBuilder.UseSqlServer(
            "Server=sqlserver;Database=ScratchGame;User Id=sa;Password=Str0ng@P@ssw0rd;Encrypt=false;TrustServerCertificate=true;");

        return new AppDbContext(optionsBuilder.Options);
    }
}