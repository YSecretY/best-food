using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BestFood.Modules.Shared.Presentation;

public static class PresentationConfiguration
{
    public static IServiceCollection AddSharedPresentation(this IServiceCollection services)
    {
        services.AddMapster();

        return services;
    }
}