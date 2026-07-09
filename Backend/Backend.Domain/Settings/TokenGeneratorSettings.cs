namespace Backend.Domain.Settings;

public class TokenGeneratorSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public TimeSpan AccessTokenExpiration { get; set; }
    public TimeSpan RefreshTokenExpiration { get; set; }
}