using Backend.Domain.Models;


namespace Backend.DataAccess.Storages.DTO;

public class UserSessionDto
{
    public int UserId { get; set; }
    public string Login { get; set; } = string.Empty;

    public UserSessionDto()
    {
    }

    public UserSessionDto(User user)
    {
        UserId = user.Id;
        Login = user.Login;
    }
}