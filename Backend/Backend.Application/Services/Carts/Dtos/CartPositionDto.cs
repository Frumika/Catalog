namespace Backend.Application.Services.Carts.Dtos;

public class CartPositionDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }

    // Базовая цена за 1 шт.
    public int BasePrice { get; set; }

    // Процент скидки (0, если нет)
    public byte DiscountPercent { get; set; }

    // Цена за 1 шт. с учетом скидки
    public int PriceWithDiscount { get; set; }

    // Итоговая базовая цена позиции (BasePrice * Quantity)
    public int PositionBaseTotal => BasePrice * Quantity;

    // Итоговая цена позиции со скидкой (PriceWithDiscount * Quantity)
    public int PositionTotal => PriceWithDiscount * Quantity;

    // Выгода (сколько сэкономил на этой позиции)
    public int PositionDiscountAmount => PositionBaseTotal - PositionTotal;
}