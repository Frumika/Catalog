namespace Backend.Application.Services.Auth.Dtos;

public class SessionDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}