using Backend.Domain.Models;


namespace Backend.Application.DTO.Entities.Order;

public class OrderStateDto
{
    public List<OrderItem> OrderItems { get; set; }
    public decimal FinalPrice { get; set; }

    public OrderStateDto(DataAccess.Storages.DTO.OrderStateDto orderState)
    {
        OrderItems = orderState.OrderItems.ToList();
        FinalPrice = orderState.FinalPrice;
    }
}