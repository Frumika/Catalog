namespace Backend.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public DateTime DeletionTime { get; set; }
    public DateTime? PaidAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PickupPointId { get; set; }
    public PickupPoint PickupPoint { get; set; } = null!;

    public ICollection<OrderPosition> OrderPositions { get; set; } = null!;
}