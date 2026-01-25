using System.Text.Json;
using Backend.DataAccess.Redis;
using Backend.DataAccess.Storages.DTO;
using StackExchange.Redis;


namespace Backend.DataAccess.Storages;

public class CartStateStorage
{
    private const string StateKey = "cart:state";
    
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(30);

    public CartStateStorage(RedisDbProvider redis)
    {
        _database = redis.CartStates;
    }

    public async Task<bool> SetStateAsync(CartStateDto state)
    {
        try
        {
            string stateKey = $"{StateKey}:{state.UserId}";
            string json = JsonSerializer.Serialize(state);
            
            bool isStateCreated = await _database.StringSetAsync(stateKey, json, _expiryTime, When.NotExists);
            return isStateCreated;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the state", ex);
        }
    }

    public async Task<bool> UpdateStateAsync(CartStateDto state)
    {
        try
        {
            string stateKey = $"{StateKey}:{state.UserId}";
            bool isStateExist = await _database.KeyExistsAsync(stateKey);
            if (!isStateExist) return false;
           
            string json = JsonSerializer.Serialize(state);
            bool isStateSet = await _database.StringSetAsync(stateKey, json, _expiryTime);
            return isStateSet;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while updating the state", ex);
        }
    }

    public async Task<CartStateDto?> GetStateAsync(int userId)
    {
        if (userId <= 0) throw new ArgumentException("User Id must be greater than 0");

        try
        {
            string stateKey = $"{StateKey}:{userId}";
            string? json = await _database.StringGetAsync(stateKey);
            if (json is null) return null;

            CartStateDto? state = JsonSerializer.Deserialize<CartStateDto>(json);
            return state;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while getting the state", ex);
        }
    }

    public async Task<bool> RefreshStateTimeAsync(int userId)
    {
        if (userId <= 0) throw new ArgumentException("User Id must be greater than 0");

        try
        {
            string stateKey = $"{StateKey}:{userId}";
            bool isStateRefreshed = await _database.KeyExpireAsync(stateKey, _expiryTime);
            return isStateRefreshed;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while refreshing of state time", ex);
        }
    }

    public async Task<bool> DeleteStateAsync(int userId)
    {
        if (userId <= 0) throw new ArgumentException("User Id must be greater than 0");

        try
        {
            string stateKey = $"{StateKey}:{userId}";
            bool isStateDeleted = await _database.KeyDeleteAsync(stateKey);
            return isStateDeleted;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while deleting the state", ex);
        }
    }
}