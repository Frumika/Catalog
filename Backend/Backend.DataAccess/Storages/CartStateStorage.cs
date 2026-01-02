using System.Text.Json;
using Backend.DataAccess.Redis;
using Backend.DataAccess.Storages.DTO;
using Backend.DataAccess.Storages.Interfaces;
using StackExchange.Redis;


namespace Backend.DataAccess.Storages;

public class CartStateStorage : ICartStateStorage
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(30);

    private const string SessionKey = "cart:session";

    public CartStateStorage(RedisDbProvider redis)
    {
        _database = redis.CartSessions;
    }

    public async Task<bool> SetSessionAsync(string sessionId, CartStateDto state)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            string json = JsonSerializer.Serialize(state);
            return await _database.StringSetAsync($"{SessionKey}:{sessionId}", json, _expiryTime);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the session", ex);
        }
    }

    public async Task<CartStateDto?> GetSessionAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            string? json = await _database.StringGetAsync($"{SessionKey}:{sessionId}");
            if (json is null) return null;

            return JsonSerializer.Deserialize<CartStateDto>(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while getting the session", ex);
        }
    }

    public async Task<bool> RefreshSessionTimeAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            bool refreshed = await _database.KeyExpireAsync($"{SessionKey}:{sessionId}", _expiryTime);
            return refreshed;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while refreshing of session time", ex);
        }
    }

    public async Task<bool> DeleteSessionAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            bool deleted = await _database.KeyDeleteAsync($"{SessionKey}:{sessionId}");
            return deleted;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while deleting the session", ex);
        }
    }
}