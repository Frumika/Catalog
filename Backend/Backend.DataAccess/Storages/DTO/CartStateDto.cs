namespace Backend.DataAccess.Storages.DTO;

public class CartStateDto
{
    public int UserId { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}