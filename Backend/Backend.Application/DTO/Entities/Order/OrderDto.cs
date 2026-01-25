namespace Backend.Application.DTO.Entities.Order;

public class OrderDto
{
    public List<OrderItemDto> OrderItems { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}