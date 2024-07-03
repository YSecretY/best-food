using BestFood.Modules.Users.Domain.Users;

namespace BestFood.Modules.Users.Application.Services.Jwt;

public interface IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string email, UserRole userRole);

    public string GenerateEmailConfirmationToken(string email);
}