namespace Backend.Domain.Models;

public class WishlistItem
{
    public int Id { get; set; }
    public DateTime AddedAt { get; set; }
    
    public int WishlistId { get; set; }
    public int ProductId { get; set; }

    public Wishlist Wishlist { get; set; } = null!;
    public Product Product { get; set; } = null!;
}