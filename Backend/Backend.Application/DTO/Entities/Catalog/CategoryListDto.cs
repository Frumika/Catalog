namespace Backend.Application.DTO.Entities.Catalog;

public class CategoryListDto
{
    public List<CategoryDto> Categories { get; set; }

    public CategoryListDto(IEnumerable<CategoryDto> categories)
    {
        Categories = categories.ToList();
    }
}