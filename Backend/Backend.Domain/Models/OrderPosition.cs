namespace Backend.Domain.Models;

public class OrderPosition
{
    public int Quantity { get; set; }
    public byte DiscountPercent { get; set; }
    public decimal Price { get; set; }
    public DateTime DeliveryDate { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}