namespace Backend.Application.Services.PickupPoints.Dtos;

public class PickupPointDto
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int ShelfLifetime { get; set; }
    public DateTime SelectedAt { get; set; }
}