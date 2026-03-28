namespace Backend.Application.DTO.Catalog;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public CategoryDto(Domain.Models.Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}