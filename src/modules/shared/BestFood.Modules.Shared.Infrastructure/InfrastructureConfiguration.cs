using System.Text.Json;
using BestFood.Modules.Shared.Application.Emails;
using BestFood.Modules.Shared.Application.Events;
using BestFood.Modules.Shared.Application.Time;
using BestFood.Modules.Shared.Infrastructure.Emails;
using BestFood.Modules.Shared.Infrastructure.Events;
using BestFood.Modules.Shared.Infrastructure.Time;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BestFood.Modules.Shared.Infrastructure;

public static class InfrastructureConfiguration
{
    public static void AddSharedInfrastructure(this IServiceCollection services, Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        services.TryAddSingleton<IEventBus, EventBus>();

        services.AddMassTransit(configure =>
        {
            foreach (var configureConsumer in moduleConfigureConsumers)
                configureConsumer(configure);

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((ctx, cfg) => { cfg.ConfigureEndpoints(ctx); });
        });
    }
}