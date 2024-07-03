using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BestFood.Modules.Shared.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddSharedApplication(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}