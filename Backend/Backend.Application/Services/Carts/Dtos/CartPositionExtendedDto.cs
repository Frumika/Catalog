namespace Backend.Application.Services.Carts.Dtos;

public class CartPositionExtendedDto : CartPositionDto
{
    public string ProductName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    // Базовая цена за 1 шт.
    public int BasePrice { get; set; }

    // Процент скидки (0, если нет)
    public byte DiscountPercent { get; set; }

    // Цена за 1 шт. с учетом скидки
    public int DiscountedPrice { get; set; }
}