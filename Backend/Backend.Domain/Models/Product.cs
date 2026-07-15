namespace Backend.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public byte DiscountPercent { get; set; }
    public int Quantity { get; set; }

    public int MakerId { get; set; }
    public int CategoryId { get; set; }

    public Seller Seller { get; set; } = null!;
    public Category Category { get; set; } = null!;

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<WishedProduct> WishlistItems { get; set; } = new List<WishedProduct>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}