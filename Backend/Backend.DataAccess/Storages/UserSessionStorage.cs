using System.Text.Json;
using Backend.DataAccess.Redis;
using Backend.Domain.Settings;
using StackExchange.Redis;


namespace Backend.DataAccess.Storages;

public class UserSessionStorage
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime;

    private const string SessionKey = "auth:session";
    private const string IndexKey = "auth:user_sessions";

    public UserSessionStorage(UserSettings settings, RedisDbProvider redis)
    {
        _database = redis.UserSessions;
        _expiryTime = settings.SessionLifetime;
    }

    public async Task<string?> SetSessionAsync(int userId)
    {
        string sessionId = Guid.NewGuid().ToString();

        string sessionKey = $"{SessionKey}:{sessionId}";
        string indexKey = $"{IndexKey}:{userId}";
        try
        {
            var sessions = await _database.SetMembersAsync(indexKey);
            foreach (var session in sessions)
            {
                bool isExist = await _database.KeyExistsAsync($"{SessionKey}:{session}");
                if (!isExist) await _database.SetRemoveAsync(indexKey, session);
            }

            var transaction = _database.CreateTransaction();

            _ = transaction.StringSetAsync(sessionKey, userId, _expiryTime);
            _ = transaction.SetAddAsync(indexKey, sessionId);

            bool committed = await transaction.ExecuteAsync();
            return committed ? sessionId : null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the session", ex);
        }
    }

    public async Task<int?> GetUserIdAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("Session ID cannot be null or empty");

        try
        {
            RedisValue userId = await _database.StringGetAsync($"{SessionKey}:{sessionId}");
            return userId.HasValue ? (int)userId : null;
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

            RedisValue userId = await _database.StringGetAsync(sessionKey);
            if (!userId.HasValue) return false;

            var indexKey = $"{IndexKey}:{(int)userId}";

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