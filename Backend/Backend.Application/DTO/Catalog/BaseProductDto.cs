namespace Backend.Application.DTO.Catalog;

public class BaseProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ReviewCount { get; set; }
    public double AverageScore { get; set; }
}