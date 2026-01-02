using StackExchange.Redis;


namespace Backend.DataAccess.Redis;

public class RedisDbProvider
{
    private readonly IConnectionMultiplexer _userConnection;
    private readonly IConnectionMultiplexer _cartConnection;

    public IDatabase UserSessions => _userConnection.GetDatabase();
    public IDatabase CartSessions => _cartConnection.GetDatabase();

    public RedisDbProvider(IConnectionMultiplexer userConnection, IConnectionMultiplexer cartConnection)
    {
        _userConnection = userConnection;
        _cartConnection = cartConnection;
    }
}