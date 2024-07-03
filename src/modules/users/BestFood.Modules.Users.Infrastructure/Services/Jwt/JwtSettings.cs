namespace BestFood.Modules.Users.Infrastructure.Services.Jwt;

public class JwtSettings
{
    public const string SectionName = nameof(JwtSettings);

    public string Secret { get; init; } = null!;

    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;

    public string EmailAudience { get; init; } = null!;

    public int ExpirationInMinutes { get; init; }
}