namespace Backend.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public int MakerId { get; set; }
    public int CategoryId { get; set; }
    
    public Maker Maker { get; set; } = null!;
    public Category Category { get; set; } = null!;

    public ICollection<ProductImage> ProductImages { get; set; } = null!;
    public ICollection<OrderedProduct> OrderedProducts { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = null!;
    public ICollection<WishlistItem> WishlistItems { get; set; } = null!;
}