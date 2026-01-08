namespace Backend.Domain.Models;

public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPrice { get; }

    public OrderItem()
    {
    }
    
    public OrderItem(Product product, int quantity)
    {
        ProductId = product.Id;
        Quantity = quantity;
        ProductPrice = product.Price;
        TotalPrice = ProductPrice * Quantity;
    }
}