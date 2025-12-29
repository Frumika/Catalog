namespace Backend.Application.DTO.Entities.User;

public class UserSessionDto
{
    public string SessionId { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string Login { get; set; } = string.Empty;
}