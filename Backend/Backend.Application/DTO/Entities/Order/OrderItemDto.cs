using Backend.Domain.Models;


namespace Backend.Application.DTO.Entities.Order;

public class OrderItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice => Price * Quantity;

    public OrderItemDto()
    {
    }
    
    public OrderItemDto(Product product, int quantity)
    {
        Id = product.Id;
        Name = product.Name;
        Quantity = quantity;
        Price = product.Price;
    }
}