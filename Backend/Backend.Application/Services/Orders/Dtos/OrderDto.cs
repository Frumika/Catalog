namespace Backend.Application.Services.Orders.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime DeliveryDate { get; set; }
}