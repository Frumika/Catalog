using Backend.DataAccess.Storages.DTO;


namespace Backend.DataAccess.Storages.Interfaces;

public interface ICartStateStorage
{
    Task<bool> SetStateAsync(CartStateDto state);
    Task<bool> UpdateStateAsync(CartStateDto state);
    Task<CartStateDto?> GetStateAsync(int userId);
    Task<bool> RefreshStateTimeAsync(int userId);
    Task<bool> DeleteStateAsync(int userId);
}