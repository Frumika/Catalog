namespace Backend.Domain.Models;

public class ProductImage
{
    public int Id { get; set; }
    public int Position { get; set; }
    public string Path { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}