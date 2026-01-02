using Backend.DataAccess.Storages.DTO;


namespace Backend.DataAccess.Storages.Interfaces;

public interface IUserSessionStorage
{
    Task SetSessionAsync(string sessionId, UserSessionDto state);
    Task<UserSessionDto?> GetSessionAsync(string sessionId);
    Task <bool> RefreshSessionTimeAsync(string sessionId);
    Task<bool> LogoutSessionAsync(string sessionId);
    Task<bool> LogoutAllSessionsAsync(int id);
}