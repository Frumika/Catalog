namespace Backend.Application.Services.Carts.Dtos;

public class CartDto
{
    public List<CartPositionDto> Items { get; set; } = new();

    public int TotalQuantity { get; set; }
    public int TotalBasePrice { get; set; }
    public int TotalDiscountAmount { get; set; }
    public int FinalPrice => TotalBasePrice - TotalDiscountAmount;
}