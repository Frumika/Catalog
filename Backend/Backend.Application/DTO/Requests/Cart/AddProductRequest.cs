namespace Backend.Application.DTO.Requests.Cart;

public class AddProductRequest
{
    public long UserId { get; set; }
    public int ProductId { get; set; }
}