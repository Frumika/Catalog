namespace Backend.Application.DTO.Order;

public class OrderDto
{
    public List<OrderItemDto> OrderItems { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}