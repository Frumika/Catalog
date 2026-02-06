namespace Backend.Application.DTO.Entities.Catalog;

public class ProductExtendedDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ProductDescription { get; set; }

    public string MakerName { get; set; } = string.Empty;
    public string? MakerDescription { get; set; }

    public List<string> ImageUrls { get; set; } = null!;
}