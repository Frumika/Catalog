namespace Backend.Application.Services.Catalog.Dtos;

public class BaseProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Price { get; set; }
    public int DiscountPrice { get; set; }
    public byte DiscountPercent { get; set; }
    public int ReviewCount { get; set; }
    public double AverageScore { get; set; }
}