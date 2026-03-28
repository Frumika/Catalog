namespace Backend.Application.DTO.Cart;

public class CartDto
{
    public List<CartItem> CartItems { get; set; } = new();
    public decimal TotalPrice { get; set; }
}