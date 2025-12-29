using Backend.Domain.Models;

namespace Backend.Application.DTO.Entities.Product;

public class ProductExtendedDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ProductDescription { get; set; }
    
    public string MakerName { get; set; } = string.Empty;
    public string? MakerDescription { get; set; }

    public ProductExtendedDto()
    {
    }

    public ProductExtendedDto(Domain.Models.Product product, Maker maker)
    {
        Id = product.Id;
        ProductName = product.Name;
        Price = product.Price;
        ProductDescription = product.Description;
        MakerName = maker.Name;
        MakerDescription = maker.Description;
    }
}