using StackExchange.Redis;


namespace Backend.DataAccess.Redis;

public class RedisDbProvider
{
    private readonly IConnectionMultiplexer _userConnection;
    public IDatabase UserSessions => _userConnection.GetDatabase();

    public RedisDbProvider(IConnectionMultiplexer userConnection)
    {
        _userConnection = userConnection;
    }
}