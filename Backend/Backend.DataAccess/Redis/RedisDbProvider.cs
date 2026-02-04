using StackExchange.Redis;


namespace Backend.DataAccess.Redis;

public class RedisDbProvider
{
    private const int UserSessionsDbIndex = 0;
    private const int OrdersStatesDbIndex = 1;
    
    private readonly IConnectionMultiplexer _dbConnection;
    
    public IDatabase UserSessions => _dbConnection.GetDatabase(UserSessionsDbIndex);
    public IDatabase OrderStates => _dbConnection.GetDatabase(OrdersStatesDbIndex);

    public RedisDbProvider(IConnectionMultiplexer dbConnection)
    {
        _dbConnection = dbConnection;
    }
}