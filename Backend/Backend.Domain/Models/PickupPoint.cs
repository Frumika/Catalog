namespace Backend.Domain.Models;

public class PickupPoint
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public StreetType StreetType { get; set; }
    public string StreetName { get; set; } = string.Empty;
    public string House { get; set; } = string.Empty;
    public string? Building { get; set; }
    
    //Todo: дописать в MainDbContext
    public int ShelfLifetime { get; set; }
    public DateTime AddedAt { get; set; }

    public ICollection<UserPickupPoint> UserPickupPoints { get; set; } = new List<UserPickupPoint>();
}