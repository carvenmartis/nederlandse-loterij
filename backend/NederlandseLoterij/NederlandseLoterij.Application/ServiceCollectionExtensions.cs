using Microsoft.Extensions.DependencyInjection;
using NederlandseLoterij.Application.SratchSquares.Commands;

namespace NederlandseLoterij.Application;

/// <summary>
/// Provides extension methods for configuring application services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds application services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ScratchSquareCommandHandler).Assembly));

        return services;
    }
}