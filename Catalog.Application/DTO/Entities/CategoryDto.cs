using Catalog.Domain.Models;

namespace Catalog.Application.DTO.Entities;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public CategoryDto()
    {
    }

    public CategoryDto(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}