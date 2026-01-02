using Backend.DataAccess.Redis;
using Backend.DataAccess.Sessions.DTO;
using Backend.DataAccess.Sessions.Storages.Interfaces;
using StackExchange.Redis;


namespace Backend.DataAccess.Sessions.Storages;

public class CartSessionStorage : ICartSessionStorage
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(30);

    public CartSessionStorage(RedisDbProvider redis)
    {
        _database = redis.CartSessions;
    }

    public Task SetSessionAsync(string sessionId, CartStateDto state)
    {
        throw new NotImplementedException();
    }

    public Task<CartStateDto?> GetSessionAsync(string sessionId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RefreshSessionTimeAsync(string sessionId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSessionAsync(string sessionId)
    {
        throw new NotImplementedException();
    }
}