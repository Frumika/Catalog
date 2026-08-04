namespace Backend.Application.Services.Orders.Dtos;

public class ExtendedOrderDto : OrderDto
{
    public List<OrderPositionDto> OrderPositions { get; set; } = new();
}