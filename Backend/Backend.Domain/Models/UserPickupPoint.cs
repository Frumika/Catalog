namespace Backend.Domain.Models;

public class UserPickupPoint
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PickupPointId { get; set; }
    public PickupPoint PickupPoint { get; set; } = null!;
    
    public DateTime SelectedAt { get; set; }
    public DateTime AddedAt { get; set; }
}