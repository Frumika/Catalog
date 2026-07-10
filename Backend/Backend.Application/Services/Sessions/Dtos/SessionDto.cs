namespace Backend.Application.Services.Sessions.Dtos;

public class SessionDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}