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

    private const string StateKey = "cart:state";
    private const string IndexKey = "cart:state:user";

    public CartStateStorage(RedisDbProvider redis)
    {
        _database = redis.CartSessions;
    }

    public async Task<bool> SetStateAsync(CartStateDto state)
    {
        string stateId = Guid.NewGuid().ToString();

        string stateKey = $"{StateKey}:{stateId}";
        string indexKey = $"{IndexKey}:{state.UserId}";

        try
        {
            string json = JsonSerializer.Serialize(state);

            bool isIndexExist = await _database.KeyExistsAsync(indexKey);

            ITransaction transaction = _database.CreateTransaction();

            if (!isIndexExist) _ = transaction.StringSetAsync(indexKey, stateId, _expiryTime);
            _ = transaction.StringSetAsync(stateKey, json, _expiryTime);

            bool commited = await transaction.ExecuteAsync();
            return commited;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the session", ex);
        }
    }

    public async Task<bool> UpdateStateAsync(CartStateDto state)
    {
        try
        {
            string? stateId = await _database.StringGetAsync($"{IndexKey}:{state.UserId}");
            if (stateId is null) return false;

            string stateKey = $"{StateKey}:{stateId}";
            string indexKey = $"{IndexKey}:{state.UserId}";

            string json = JsonSerializer.Serialize(state);

            ITransaction transaction = _database.CreateTransaction();
            _ = transaction.StringSetAsync(stateKey, json, _expiryTime);
            _ = transaction.KeyExpireAsync(indexKey, _expiryTime);

            bool commited = await transaction.ExecuteAsync();
            return commited;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while updating the session", ex);
        }
    }

    public async Task<CartStateDto?> GetStateAsync(int userId)
    {
        if (userId <= 0) throw new ArgumentException("User Id must be greater than 0");

        try
        {
            string? stateId = await _database.StringGetAsync($"{IndexKey}:{userId}");
            if (stateId is null) return null;

            string? json = await _database.StringGetAsync($"{StateKey}:{stateId}");
            if (json is null) return null;

            CartStateDto? state = JsonSerializer.Deserialize<CartStateDto>(json);
            return state;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while getting the session", ex);
        }
    }

    public async Task<bool> RefreshStateTimeAsync(int userId)
    {
        if (userId <= 0) throw new ArgumentException("User Id must be greater than 0");

        try
        {
            string? stateId = await _database.StringGetAsync($"{IndexKey}:{userId}");
            if (stateId is null) return false;

            string stateKey = $"{StateKey}:{stateId}";
            string indexKey = $"{IndexKey}:{userId}";

            ITransaction transaction = _database.CreateTransaction();

            _ = transaction.KeyExpireAsync(indexKey, _expiryTime);
            _ = transaction.KeyExpireAsync(stateKey, _expiryTime);

            bool commited = await transaction.ExecuteAsync();
            return commited;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while refreshing of session time", ex);
        }
    }

    public async Task<bool> DeleteStateAsync(int userId)
    {
        if (userId <= 0) throw new ArgumentException("User Id must be greater than 0");

        try
        {
            string? stateId = await _database.StringGetAsync($"{IndexKey}:{userId}");
            if (stateId is null) return false;
            
            string stateKey = $"{StateKey}:{stateId}";
            string indexKey = $"{IndexKey}:{userId}";

            ITransaction transaction = _database.CreateTransaction();

            _ = transaction.KeyDeleteAsync(indexKey);
            _ = transaction.KeyDeleteAsync(stateKey);

            bool commited = await transaction.ExecuteAsync();
            return commited;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while deleting the session", ex);
        }
    }
}