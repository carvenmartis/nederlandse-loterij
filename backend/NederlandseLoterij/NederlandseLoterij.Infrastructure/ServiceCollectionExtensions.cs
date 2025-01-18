using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Infrastructure.Repositories;

namespace NederlandseLoterij.Infrastructure;

/// <summary>
/// Extension methods for configuring services in the IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds infrastructure services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="connectionString">The connection string for the database.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Register DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register repositories
        services.AddScoped<IGridRepository, GridRepository>();

        return services;
    }
}