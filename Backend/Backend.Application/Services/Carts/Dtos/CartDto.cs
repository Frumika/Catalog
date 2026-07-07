namespace Backend.Application.Services.Carts.Dtos;

public class CartDto
{
    public List<CartItemDto> CartItems { get; set; } = new();
    public decimal TotalPrice { get; set; }
}