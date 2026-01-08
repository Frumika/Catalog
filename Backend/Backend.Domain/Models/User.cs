namespace Backend.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = null!;
}