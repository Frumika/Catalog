using Backend.Domain.Models;


namespace Backend.DataAccess.Sessions.DTO;

public class UserSessionStateDto
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;

    public UserSessionStateDto()
    {
    }

    public UserSessionStateDto(User user)
    {
        Id = user.Id;
        Login = user.Login;
    }
}