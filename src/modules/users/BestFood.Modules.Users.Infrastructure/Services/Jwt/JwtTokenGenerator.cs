using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BestFood.Modules.Shared.Application.Time;
using BestFood.Modules.Users.Application.Services.Jwt;
using BestFood.Modules.Users.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BestFood.Modules.Users.Infrastructure.Services.Jwt;

public class JwtTokenGenerator(
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtSettings> jwtOptions
) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public string GenerateToken(Guid userId, string email, UserRole userRole)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, Enum.GetName(userRole) ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingCredentials = new SigningCredentials
        (
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret).ToArray()),
            SecurityAlgorithms.HmacSha256
        );

        var securityToken = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: dateTimeProvider.CurrentTime.AddMinutes(_jwtSettings.ExpirationInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateEmailConfirmationToken(string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingCredentials = new SigningCredentials
        (
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret).ToArray()),
            SecurityAlgorithms.HmacSha256
        );

        var securityToken = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.EmailAudience,
            expires: dateTimeProvider.CurrentTime.AddMinutes(_jwtSettings.ExpirationInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}