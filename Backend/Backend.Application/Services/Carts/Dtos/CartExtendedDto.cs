namespace Backend.Application.Services.Carts.Dtos;

public class CartExtendedDto: CartDto<CartPositionExtendedDto>
{
    public int TotalBasePrice { get; set; }
    public int TotalDiscountAmount { get; set; }
    public int FinalPrice => TotalBasePrice - TotalDiscountAmount;
}