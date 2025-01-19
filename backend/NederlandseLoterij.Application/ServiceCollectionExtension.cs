using Microsoft.Extensions.DependencyInjection;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Application.Services;

namespace NederlandseLoterij.Application;

/// <summary>
/// Extension methods for setting up application services in an <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds application services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IScratchService, ScratchService>();
        services.AddHostedService<BackgroundSquareScratcher>();

        return services;
    }
}