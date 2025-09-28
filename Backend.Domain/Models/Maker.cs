namespace Backend.Domain.Models;

public class Maker
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Country { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Description { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}