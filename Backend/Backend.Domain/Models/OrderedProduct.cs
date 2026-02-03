namespace Backend.Domain.Models;

public class OrderedProduct
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;

    public OrderedProduct()
    {
    }

    public OrderedProduct(Product product, int quantity)
    {
        ProductId = product.Id;
        ProductPrice = product.Price;
        Quantity = quantity;
    }
}