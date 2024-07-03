using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BestFood.Modules.Users.Application.Services.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BestFood.Modules.Users.Infrastructure.Services.Jwt;

public class JwtTokenValidator(
    IOptions<JwtSettings> jwtOptions
) : IJwtTokenValidator
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<bool> IsValidEmailConfirmationTokenAsync(string jwtToken)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.EmailAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret).ToArray()),
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationResult = await jwtSecurityTokenHandler.ValidateTokenAsync(jwtToken, validationParameters);

        return tokenValidationResult.IsValid;
    }
}