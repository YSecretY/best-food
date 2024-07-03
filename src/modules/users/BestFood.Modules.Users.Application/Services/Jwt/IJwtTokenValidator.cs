namespace BestFood.Modules.Users.Application.Services.Jwt;

public interface IJwtTokenValidator
{
    public Task<bool> IsValidEmailConfirmationTokenAsync(string jwtToken);
}