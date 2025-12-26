namespace Backend.Domain.Models;

public class User
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
}