using Backend.DataAccess.Redis;
using StackExchange.Redis;


namespace Backend.DataAccess.Storages;

public class OrderIndexStorage
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(5);

    private const string StateKeyPrefix = "order:state";

    public OrderIndexStorage(RedisDbProvider redis)
    {
        _database = redis.OrderStates;
    }


    public async Task<bool> IsOrderExists(string userSessionId)
    {
        try
        {
            string stateKey = $"{StateKeyPrefix}:{userSessionId}";
            bool isOrderExist = await _database.KeyExistsAsync(stateKey);
            return isOrderExist;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while checking the order", ex);
        }
    }

    public async Task<bool> SetStateAsync(string userSessionId, int orderId)
    {
        try
        {
            string stateKey = $"{StateKeyPrefix}:{userSessionId}";
            bool isStateSet = await _database.StringSetAsync(stateKey, orderId, _expiryTime, When.NotExists);
            return isStateSet;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while setting the order", ex);
        }
    }

    public async Task<int?> GetOrderIdAsync(string userSessionId)
    {
        if (string.IsNullOrWhiteSpace(userSessionId)) throw new ArgumentException("User session id mustn't be empty");

        try
        {
            string stateKey = $"{StateKeyPrefix}:{userSessionId}";
            RedisValue value = await _database.StringGetAsync(stateKey);

            if (!value.HasValue) return null;
            return (int)value;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while getting the order", ex);
        }
    }

    public async Task<bool> DeleteStateAsync(string userSessionId)
    {
        if (string.IsNullOrWhiteSpace(userSessionId)) throw new ArgumentException("User session id mustn't be empty");

        try
        {
            string stateKey = $"{StateKeyPrefix}:{userSessionId}";
            bool isKeyDeleted = await _database.KeyDeleteAsync(stateKey);
            return isKeyDeleted;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while deleting the order", ex);
        }
    }
}