namespace Backend.Domain.Models;

public class WishlistItem
{
    public int Id { get; set; }
    public DateTime AddedAt { get; set; }
    
    public int WishlistId { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;
}