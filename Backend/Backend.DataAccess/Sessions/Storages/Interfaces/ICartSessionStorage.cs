using Backend.DataAccess.Sessions.DTO;

namespace Backend.DataAccess.Sessions.Storages.Interfaces;

public interface ICartSessionStorage
{
    Task SetSessionAsync(string sessionId, CartStateDto state);
    Task<CartStateDto?> GetSessionAsync(string sessionId);
    Task<bool> RefreshSessionTimeAsync(string sessionId);
    Task<bool> DeleteSessionAsync(string sessionId);
}