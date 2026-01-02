using System.Text.Json;
using Backend.DataAccess.Redis;
using Backend.DataAccess.Storages.DTO;
using Backend.DataAccess.Storages.Interfaces;
using StackExchange.Redis;


namespace Backend.DataAccess.Storages;

public class UserSessionStorage : IUserSessionStorage
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(15);

    private const string SessionKey = "auth:session";
    private const string IndexKey = "auth:user_sessions";

    public UserSessionStorage(RedisDbProvider redis)
    {
        _database = redis.UserSessions;
    }

    public async Task SetSessionAsync(string sessionId, UserSessionDto state)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        string sessionKey = $"{SessionKey}:{sessionId}";
        string indexKey = $"{IndexKey}:{state.Id}";

        try
        {
            string json = JsonSerializer.Serialize(state);

            var transaction = _database.CreateTransaction();

            _ = transaction.StringSetAsync(sessionKey, json, _expiryTime);
            _ = transaction.SetAddAsync(indexKey, sessionId);

            bool committed = await transaction.ExecuteAsync();

            if (!committed) throw new InvalidOperationException("Failed to set session for sessionId");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the session", ex);
        }
    }

    public async Task<UserSessionDto?> GetSessionAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            string? json = await _database.StringGetAsync($"{SessionKey}:{sessionId}");

            if (string.IsNullOrEmpty(json)) return null;
            return JsonSerializer.Deserialize<UserSessionDto>(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while retrieving the session", ex);
        }
    }

    public async Task<bool> RefreshSessionTimeAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        string sessionKey = $"{SessionKey}:{sessionId}";

        try
        {
            bool updated = await _database.KeyExpireAsync(sessionKey, _expiryTime);
            return updated;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while refreshing of session time", ex);
        }
    }

    public async Task<bool> LogoutSessionAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            var sessionKey = $"{SessionKey}:{sessionId}";
            var json = await _database.StringGetAsync(sessionKey);
            if (json.IsNullOrEmpty) return false;

            var state = JsonSerializer.Deserialize<UserSessionDto>(json!);
            if (state is null) return false;

            var indexKey = $"{IndexKey}:{state.Id}";

            var transaction = _database.CreateTransaction();
            _ = transaction.KeyDeleteAsync(sessionKey);
            _ = transaction.SetRemoveAsync(indexKey, sessionId);

            bool committed = await transaction.ExecuteAsync();
            return committed;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while deleting the session", ex);
        }
    }

    public async Task<bool> LogoutAllSessionsAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("User ID must be greater than 0");

        try
        {
            string indexKey = $"{IndexKey}:{id}";
            var sessions = await _database.SetMembersAsync(indexKey);

            var transaction = _database.CreateTransaction();

            foreach (var sessionId in sessions)
                _ = transaction.KeyDeleteAsync($"{SessionKey}:{sessionId}");
            _ = transaction.KeyDeleteAsync(indexKey);

            bool committed = await transaction.ExecuteAsync();
            return committed;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while deleting all sessions", ex);
        }
    }
}