namespace Backend.Application.Services.Catalog.Dtos;

public class CategoryListDto
{
    public List<CategoryDto> Categories { get; set; }

    public CategoryListDto(IEnumerable<CategoryDto> categories)
    {
        Categories = categories.ToList();
    }
}