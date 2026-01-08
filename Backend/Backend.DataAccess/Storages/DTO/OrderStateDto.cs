using Backend.Domain.Models;


namespace Backend.DataAccess.Storages.DTO;

public class OrderStateDto
{
    public int UserId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal FinalPrice { get; set; }
    
}