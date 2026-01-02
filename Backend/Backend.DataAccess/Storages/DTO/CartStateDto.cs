namespace Backend.DataAccess.Storages.DTO;

public class CartStateDto
{
    public long UserId { get; set; }
    public List<ProductDto>? Products { get; set; }
}