namespace Backend.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public int MakerId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public required Maker Maker { get; set; }
    public required Category Category { get; set; }
}