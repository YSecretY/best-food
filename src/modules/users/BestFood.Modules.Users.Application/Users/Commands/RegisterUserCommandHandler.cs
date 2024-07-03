using BestFood.Modules.Shared.Application.Events;
using BestFood.Modules.Shared.Application.Time;
using BestFood.Modules.Users.Application.Services.Password;
using BestFood.Modules.Users.Domain.Users;
using BestFood.Modules.Users.Domain.Users.Database;
using BestFood.Modules.Users.Domain.Users.DomainEvents;
using MediatR;

namespace BestFood.Modules.Users.Application.Users.Commands;

public sealed class RegisterUserCommandHandler(
    IUsersDbContext dbContext,
    IPasswordService passwordService,
    IDateTimeProvider dateTimeProvider,
    IEventBus eventBus
) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = User.Create
        (
            email: request.Email,
            passwordHash: passwordHash,
            nickname: request.Nickname,
            role: UserRole.Default,
            createdAt: dateTimeProvider.CurrentTime
        );

        await eventBus.PublishAsync(new UserRegisteredDomainEvent(email: user.Email, nickname: user.Nickname, role: user.Role, occuredAt: dateTimeProvider.CurrentTime), cancellationToken);

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}