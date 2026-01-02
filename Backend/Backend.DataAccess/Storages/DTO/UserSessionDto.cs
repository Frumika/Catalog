using Backend.Domain.Models;


namespace Backend.DataAccess.Storages.DTO;

public class UserSessionDto
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;

    public UserSessionDto()
    {
    }

    public UserSessionDto(User user)
    {
        Id = user.Id;
        Login = user.Login;
    }
}