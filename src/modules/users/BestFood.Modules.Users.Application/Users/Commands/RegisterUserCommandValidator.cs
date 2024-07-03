using BestFood.Modules.Shared.Domain.SharedConstants;
using FluentValidation;

namespace BestFood.Modules.Users.Application.Users.Commands;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).MinimumLength(6);
        RuleFor(c => c.PasswordConfirm).Equal(c => c.Password);
        RuleFor(c => c.Nickname).NotEmpty().MaximumLength(SharedConstants.Nickname.MaxLength);
    }
}