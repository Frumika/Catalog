using StackExchange.Redis;


namespace Backend.DataAccess.Redis;

public class RedisDbProvider
{
    private readonly IConnectionMultiplexer _userConnection;
    private readonly IConnectionMultiplexer _cartConnection;
    private readonly IConnectionMultiplexer _orderConnection;

    public IDatabase UserSessions => _userConnection.GetDatabase();
    public IDatabase CartSessions => _cartConnection.GetDatabase();

    public RedisDbProvider(IConnectionMultiplexer userConnection, IConnectionMultiplexer cartConnection,
        IConnectionMultiplexer orderConnection)
    {
        _userConnection = userConnection;
        _cartConnection = cartConnection;
        _orderConnection = orderConnection;
    }
}