namespace Backend.Application.DTO.Entities.Cart;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPrice => ProductPrice * Quantity;
}