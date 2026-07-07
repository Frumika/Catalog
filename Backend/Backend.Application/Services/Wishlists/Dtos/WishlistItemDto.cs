namespace Backend.Application.Services.Wishlists.Dtos;

public class WishlistItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
}