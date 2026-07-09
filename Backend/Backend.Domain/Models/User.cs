namespace Backend.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty; 
    public string Login { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } 
    public DateTime? LastLoginAt { get; set; }
    

    public Cart Cart { get; set; } = null!;
    public Wishlist Wishlist { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<UserPickupPoint> UserPickupPoints { get; set; } = new List<UserPickupPoint>();
}