namespace Backend.Domain.Models;

public class Wishlist
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;
}