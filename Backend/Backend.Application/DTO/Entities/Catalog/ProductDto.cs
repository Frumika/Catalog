namespace Backend.Application.DTO.Entities.Catalog;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ProductDto()
    {
    }

    public ProductDto(Domain.Models.Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
    }
}