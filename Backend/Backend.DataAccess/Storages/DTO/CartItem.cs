namespace Backend.DataAccess.Storages.DTO;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    public CartItem()
    {
    }

    public CartItem(int productId, int quantity = 1)
    {
        Id = productId;
        Quantity = quantity;
    }
}