using Backend.Domain.Models;


namespace Backend.Application.Services.Orders.Dtos;

public class OrderPositionDto
{
    public int ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int Price { get; set; }
    public byte DiscountPercent { get; set; }
    public int DiscountedPrice { get; set; }
}