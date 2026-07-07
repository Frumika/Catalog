using Backend.Domain.Interfaces;
using Backend.Domain.Settings;
using StackExchange.Redis;


namespace Backend.Infrastructure.Services.Storage;

public class RedisCodeStorage : ICodeStorage
{
    private const string KeyPrefix = "verify:code";

    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime;

    public RedisCodeStorage(IConnectionMultiplexer connection, CodeStorageSettings settings)
    {
        _database = connection.GetDatabase(0);
        _expiryTime = settings.ExpirationTime;
    }

    public async Task SaveCodeAsync(string email, string code)
    {
        string key = BuildKey(email);
        await _database.StringSetAsync(key, code, _expiryTime);
    }

    public async Task<string?> GetCodeAsync(string email)
    {
        string key = BuildKey(email);
        string? hashCode = await _database.StringGetAsync(key);
        return hashCode;
    }

    public async Task RemoveCodeAsync(string email)
    {
        string key = BuildKey(email);
        await _database.KeyDeleteAsync(key);
    }

    private static string BuildKey(string email) => $"{KeyPrefix}:{email}";
}