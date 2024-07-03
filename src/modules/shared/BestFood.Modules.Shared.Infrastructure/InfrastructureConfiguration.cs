using BestFood.Modules.Shared.Application.Events;
using BestFood.Modules.Shared.Application.Time;
using BestFood.Modules.Shared.Infrastructure.Events;
using BestFood.Modules.Shared.Infrastructure.Time;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BestFood.Modules.Shared.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.TryAddSingleton<IEventBus, EventBus>();

        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((ctx, cfg) => { cfg.ConfigureEndpoints(ctx); });
        });

        return services;
    }
}