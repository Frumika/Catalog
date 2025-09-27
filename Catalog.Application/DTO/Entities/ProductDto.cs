using Catalog.Domain.Models;

namespace Catalog.Application.DTO.Entities;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public ProductDto()
    {
    }

    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        ImageUrl = product.ImageUrl;
    }
}