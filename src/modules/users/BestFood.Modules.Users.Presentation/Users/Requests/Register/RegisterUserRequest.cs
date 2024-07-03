namespace BestFood.Modules.Users.Presentation.Users.Requests.Register;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string PasswordConfirm,
    string Nickname
);