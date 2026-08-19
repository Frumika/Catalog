namespace Backend.Application.Services.Carts.Dtos;

public class CartPositionExtendedDto : CartPositionDto
{
    public string ProductName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int BasePrice { get; set; }
    public byte DiscountPercent { get; set; }
    public int DiscountedPrice { get; set; }
}