using BestFood.Modules.Shared.Application.Emails;
using BestFood.Modules.Users.Application.Services.Jwt;
using BestFood.Modules.Users.Domain.Users.DomainEvents;
using MassTransit;

namespace BestFood.Modules.Users.Infrastructure.Users.Events.Consumers;

public sealed class UserRegisteredEventConsumer(
    IEmailService emailService,
    IJwtTokenGenerator tokenGenerator
) : IConsumer<UserRegisteredDomainEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredDomainEvent> context)
    {
        var userEmail = context.Message.Email;

        await emailService.SendConfirmationLetterAsync(userEmail, tokenGenerator.GenerateEmailConfirmationToken(userEmail));
    }
}