namespace Backend.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;

    public Cart Cart { get; set; } = null!;
    public Wishlist Wishlist { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<UserPickupPoint> UserPickupPoints { get; set; } = new List<UserPickupPoint>();
}