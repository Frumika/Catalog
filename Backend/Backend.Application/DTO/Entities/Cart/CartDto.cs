namespace Backend.Application.DTO.Entities.Cart;

public class CartDto
{
    public List<CartItem> CartItems { get; set; } = new();
}