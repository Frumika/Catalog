namespace Backend.Application.DTO.Entities.Wishlist;

public class WishlistItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
}