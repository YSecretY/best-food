using BestFood.Modules.Shared.Application.Emails;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace BestFood.Modules.Shared.Infrastructure.Emails;

public class EmailService(
    IOptions<SmtpClientSettings> smtpClientOptions
) : IEmailService
{
    private readonly SmtpClientSettings _smtpClientSettings = smtpClientOptions.Value;

    public async Task SendConfirmationLetterAsync(string receiverEmail, string jwtConfirmationToken, CancellationToken cancellationToken = default)
    {
        var confirmEmailMessage = new MimeMessage();
        confirmEmailMessage.From.Add(new MailboxAddress(_smtpClientSettings.Name, _smtpClientSettings.Gmail));
        confirmEmailMessage.To.Add(MailboxAddress.Parse(receiverEmail));
        confirmEmailMessage.Subject = "BestFood confirmation letter.";

        var bodyBuilder = new BodyBuilder
        {
            TextBody =
                $"Hi! Welcome to the BestFood, click on the link to confirm your email. {_smtpClientSettings.BaseApplicationUrl}/users/confirm-email?EmailConfirmationToken={jwtConfirmationToken}&UserEmail={receiverEmail}"
        };
        confirmEmailMessage.Body = bodyBuilder.ToMessageBody();

        var smtpClient = new SmtpClient();

        try
        {
            await smtpClient.ConnectAsync(_smtpClientSettings.Server, _smtpClientSettings.Port, _smtpClientSettings.SslEnabled, cancellationToken);
            await smtpClient.AuthenticateAsync(_smtpClientSettings.Gmail, _smtpClientSettings.Password, cancellationToken);

            await smtpClient.SendAsync(confirmEmailMessage, cancellationToken);
        }
        catch
        {
            // ignored
        }
        finally
        {
            await smtpClient.DisconnectAsync(quit: false, cancellationToken);
        }
    }

    public Task SendEmailChangeConfirmationLetterAsync(string oldEmail, string receiverEmail, string jwtConfirmationToken)
    {
        throw new NotImplementedException();
    }
}