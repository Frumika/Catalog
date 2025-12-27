namespace Backend.Application.DTO.Entities.Product;

public class ProductListDto
{
    public List<ProductDto> Products { get; set; }
    public int TotalCount { get; set; }
    
    public ProductListDto(IEnumerable<ProductDto> products, int totalCount)
    {
        Products = products.ToList();
        TotalCount = totalCount;
    }
}