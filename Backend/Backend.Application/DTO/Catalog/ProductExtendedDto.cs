namespace Backend.Application.DTO.Catalog;

public class ProductExtendedDto : BaseProductDto
{
    public string? ProductDescription { get; set; }
    public List<string> ImageUrls { get; set; } = null!;
    
    public string MakerName { get; set; } = string.Empty;
    public string? MakerDescription { get; set; }
}