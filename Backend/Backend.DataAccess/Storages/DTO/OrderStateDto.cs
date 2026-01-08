using Backend.Domain.Models;


namespace Backend.DataAccess.Storages.DTO;

public class OrderStateDto
{
    public int UserId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();

    public OrderStateDto()
    {
    }

    public OrderStateDto(int userId)
    {
        UserId = userId;
    }
}