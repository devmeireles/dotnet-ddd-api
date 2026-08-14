using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using ChefHero.Application.Auth.Token;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChefHero.Infrastructure.Auth.Token;

public class JwtTokenService : ITokenService
{
    private readonly JwtOptions _options;

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(Guid userId, string email, string role)
    {
        List<Claim> claims =
        [
            new Claim(
                JwtRegisteredClaimNames.Sub,
                userId.ToString()),

            new Claim(
                JwtRegisteredClaimNames.Email,
                email),

            new Claim(
                ClaimTypes.Role,
                role)
        ];

        SymmetricSecurityKey securityKey = new(
            Encoding.UTF8.GetBytes(_options.SecretKey));

        SigningCredentials credentials = new(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        DateTime expiration = DateTime.UtcNow.AddMinutes(
            _options.ExpirationMinutes);

        JwtSecurityToken token = new(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}