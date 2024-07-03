using BestFood.Modules.Users.Application.Services.Password;

namespace BestFood.Modules.Users.Infrastructure.Services.Password;

public sealed class PasswordService : IPasswordService
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(password, passwordHash);
}