using BestFood.Modules.Users.Application.Services.Password;
using BestFood.Modules.Users.Domain.Users.Database;
using BestFood.Modules.Users.Infrastructure.Services;
using BestFood.Modules.Users.Infrastructure.Users.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestFood.Modules.Users.Infrastructure;

public static class UsersModule
{
    private static void MapControllers(this IServiceCollection services) =>
        services.AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        MapControllers(services);

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddTransient<IPasswordService, PasswordService>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("Database") ?? throw new NullReferenceException("Database connection string is not found.");

        services.AddDbContext<IUsersDbContext, UsersDbContext>(options =>
            options.UseNpgsql(dbConnectionString, optionsBuilder => optionsBuilder.MigrationsHistoryTable(Schemas.Users)));

        return services;
    }
}