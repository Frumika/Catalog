namespace Backend.Application.DTO.Entities.Auth;

public class UserSessionDto
{
    public string SessionId { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Login { get; set; } = string.Empty;
}