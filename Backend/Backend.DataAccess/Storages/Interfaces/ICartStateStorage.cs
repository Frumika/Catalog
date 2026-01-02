using Backend.DataAccess.Storages.DTO;


namespace Backend.DataAccess.Storages.Interfaces;

public interface ICartStateStorage
{
    Task<bool> SetSessionAsync(string sessionId, CartStateDto state);
    Task<CartStateDto?> GetSessionAsync(string sessionId);
    Task<bool> RefreshSessionTimeAsync(string sessionId);
    Task<bool> DeleteSessionAsync(string sessionId);
}