namespace Backend.Application.Services.Carts.Dtos;

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPrice => ProductPrice * Quantity;
}