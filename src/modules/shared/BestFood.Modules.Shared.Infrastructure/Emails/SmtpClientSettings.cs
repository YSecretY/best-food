namespace BestFood.Modules.Shared.Infrastructure.Emails;

public class SmtpClientSettings
{
    public const string SectionName = "SmtpClientOptions";

    public string Server { get; init; } = null!;

    public int Port { get; init; }

    public bool SslEnabled { get; init; }

    public string BaseApplicationUrl { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Gmail { get; init; } = null!;

    public string Password { get; init; } = null!;
}