namespace Backend.Domain.Models;

public class UserSession
{
    public int Id { get; set; }
    public string UId { get; set; } = string.Empty;

    public int UserId { get; set; }
    public int? OrderId { get; set; }

    public User User { get; set; } = null!;
    public Order? Order { get; set; }
}