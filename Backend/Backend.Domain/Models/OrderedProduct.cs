namespace Backend.Domain.Models;

public class OrderedProduct
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}