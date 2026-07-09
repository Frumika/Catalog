namespace Backend.Application.Services.Auth.Dtos;

public class UserSessionDto
{
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}