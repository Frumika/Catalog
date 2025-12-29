using Backend.DataAccess.Sessions.DTO;

namespace Backend.DataAccess.Sessions.Storages.Interfaces;

public interface IUserSessionStorage
{
    Task SetSessionAsync(string sessionId, UserSessionStateDto state);
    Task<UserSessionStateDto?> GetSessionAsync(string sessionId);
    Task<bool> LogoutSessionAsync(string sessionId);
    Task<bool> LogoutAllSessionsAsync(int id);
}