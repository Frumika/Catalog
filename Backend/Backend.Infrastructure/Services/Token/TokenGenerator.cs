using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Settings;
using Microsoft.IdentityModel.Tokens;


namespace Backend.Infrastructure.Services.Token;

public class TokenGenerator : ITokenGenerator
{
    private readonly string _secret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan _accessTokenExpiration;
    private readonly TimeSpan _refreshTokenExpiration;

    public TokenGenerator(TokenGeneratorSettings settings)
    {
        _secret = settings.Secret;
        _issuer = settings.Issuer;
        _audience = settings.Audience;
        _accessTokenExpiration = settings.AccessTokenExpiration;
        _refreshTokenExpiration = settings.RefreshTokenExpiration;
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Login),

            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow + _accessTokenExpiration,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            User = user,
            ExpiresAt = DateTime.UtcNow + _refreshTokenExpiration,
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };
    }
}