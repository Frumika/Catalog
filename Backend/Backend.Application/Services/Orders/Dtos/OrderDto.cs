namespace Backend.Application.Services.Orders.Dtos;

public class OrderDto
{
    public List<OrderItemDto> OrderItems { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}