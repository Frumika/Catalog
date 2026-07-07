namespace Backend.Application.Services.Wishlists.Dtos;

public class WishlistDto
{
    public List<WishlistItemDto> WishlistItems { get; set; } = new();
}