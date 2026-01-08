namespace Backend.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime PaymentTime { get; set; }
    public decimal FinalPrice { get; set; }
    public List<OrderItem> OrderItems { get; set; } = null!;

    public User User { get; set; } = null!;
}