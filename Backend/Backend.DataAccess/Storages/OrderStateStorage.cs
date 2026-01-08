using Backend.DataAccess.Redis;
using StackExchange.Redis;


namespace Backend.DataAccess.Storages;

public class OrderStateStorage
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(5);
    
    private const string StateKey = "order:state";
    private const string IndexKey = "order:state:user";

    public OrderStateStorage(RedisDbProvider redis)
    {
        _database = redis.OrderStates;
    }

   
}