namespace Backend.DataAccess.Sessions.DTO;

public class CartStateDto
{
    public long UserId { get; set; }
    public List<ProductDto>? Products { get; set; }
}