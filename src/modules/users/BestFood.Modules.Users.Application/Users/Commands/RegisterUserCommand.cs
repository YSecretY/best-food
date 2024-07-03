using MediatR;

namespace BestFood.Modules.Users.Application.Users.Commands;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string PasswordConfirm,
    string Nickname
) : IRequest<Guid>;