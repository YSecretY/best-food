using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BestFood.Modules.Shared.Presentation;

public static class PresentationConfiguration
{
    public static void AddSharedPresentation(this IServiceCollection services)
    {
        services.AddMapster();
    }
}