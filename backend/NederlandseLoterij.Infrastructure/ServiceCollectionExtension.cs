using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NederlandseLoterij.Application.Interfaces;
using NederlandseLoterij.Infrastructure.Repositories;

namespace NederlandseLoterij.Infrastructure;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds application services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>(options =>
            options.UseInMemoryDatabase("ScratchGameDB"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IScratchableAreaRepository, ScratchableAreaRepository>();

        return services;
    }
}