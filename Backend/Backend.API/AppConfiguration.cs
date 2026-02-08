using Backend.Domain.Settings;

namespace Backend.API;

public class AppConfiguration
{
    public OrderSettings OrderSettings { get; }
    public OrderCleanupSettings OrderCleanupSettings { get; }
    public UserSettings UserSettings { get; }

    public AppConfiguration(IConfiguration configuration)
    {
        int orderLifetime = configuration.GetValue<int>("Configuration:OrderSettings:OrderLifetimeFromMinutes");
        OrderSettings = new()
        {
            Lifetime = TimeSpan.FromMinutes(orderLifetime)
        };

        int cleanupInterval = configuration.GetValue<int>("Configuration:OrderCleanupSettings:IntervalFromMinutes");
        int cleanupBatchSize = configuration.GetValue<int>("Configuration:OrderCleanupSettings:BatchSize");
        OrderCleanupSettings = new()
        {
            Interval = TimeSpan.FromMinutes(cleanupInterval),
            BatchSize = cleanupBatchSize
        };

        int userSessionLifetime = configuration.GetValue<int>("Configuration:UserSettings:SessionLifetimeFromMinutes");
        UserSettings = new()
        {
            SessionLifetime = TimeSpan.FromMinutes(userSessionLifetime)
        };
    }
}