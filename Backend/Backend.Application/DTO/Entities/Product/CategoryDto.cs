using Backend.Domain.Models;

namespace Backend.Application.DTO.Entities.Product;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public CategoryDto(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}