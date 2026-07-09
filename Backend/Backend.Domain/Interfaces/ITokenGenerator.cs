using Backend.Domain.Models;


namespace Backend.Domain.Interfaces;

public interface ITokenGenerator
{
    public string GenerateAccessToken(User user);
    public RefreshToken GenerateRefreshToken(User user);
}