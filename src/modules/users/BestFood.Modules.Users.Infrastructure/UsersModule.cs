using System.Text;
using BestFood.Modules.Shared.Infrastructure.Emails;
using BestFood.Modules.Users.Application.Services.Jwt;
using BestFood.Modules.Users.Application.Services.Password;
using BestFood.Modules.Users.Domain.Users.Database;
using BestFood.Modules.Users.Infrastructure.Services;
using BestFood.Modules.Users.Infrastructure.Services.Jwt;
using BestFood.Modules.Users.Infrastructure.Services.Password;
using BestFood.Modules.Users.Infrastructure.Users.Database;
using BestFood.Modules.Users.Infrastructure.Users.Events.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BestFood.Modules.Users.Infrastructure;

public static class UsersModule
{
    private static void MapControllers(this IServiceCollection services) =>
        services.AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

    public static void AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        MapControllers(services);
    }
    
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredEventConsumer>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddAuth(configuration);

        services.AddConsumers(configuration);

        services.AddTransient<IPasswordService, PasswordService>();
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("Database") ?? throw new NullReferenceException("Database connection string is not found.");

        services.AddDbContext<IUsersDbContext, UsersDbContext>(options =>
            options.UseNpgsql(dbConnectionString, optionsBuilder => optionsBuilder.MigrationsHistoryTable(Schemas.Users)));
    }

    private static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();

        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddTransient<IJwtTokenValidator, JwtTokenValidator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
    }

    private static void AddConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpClientSettings>(configuration.GetRequiredSection(SmtpClientSettings.SectionName) ??
                                               throw new KeyNotFoundException("Couldn't find the smtp client configurations."));
    }
}