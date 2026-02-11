namespace Backend.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    
    public Cart Cart { get; set; } = null!;
    public Wishlist Wishlist { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = null!;
    public ICollection<UserSession> UserSessions { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;
}