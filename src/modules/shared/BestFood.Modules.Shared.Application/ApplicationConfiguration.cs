using System.Reflection;
using BestFood.Modules.Shared.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BestFood.Modules.Shared.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddSharedApplication(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            cfg.RegisterServicesFromAssemblies(assemblies);
        });

        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        return services;
    }
}