namespace Backend.Application.Services.Carts.Dtos;

public class CartDto <T>
{
    public List<T> Items { get; set; } = new();
}