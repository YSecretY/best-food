namespace BestFood.Modules.Shared.Application.Emails;

public interface IEmailService
{
    public Task SendConfirmationLetterAsync(string receiverEmail, string jwtConfirmationToken, CancellationToken cancellationToken = default);

    public Task SendEmailChangeConfirmationLetterAsync(string oldEmail, string receiverEmail, string jwtConfirmationToken);
}