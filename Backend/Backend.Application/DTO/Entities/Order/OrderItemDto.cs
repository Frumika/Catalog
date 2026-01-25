using Backend.Domain.Models;


namespace Backend.Application.DTO.Entities.Order;

public class OrderItemDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPrice { get; }

    public OrderItemDto(Product product, int quantity)
    {
        Name = product.Name;
        Quantity = quantity;
        ProductPrice = product.Price;
        TotalPrice = product.Price * quantity;
    }
}