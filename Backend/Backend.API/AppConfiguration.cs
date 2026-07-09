using Backend.Domain.Settings;


namespace Backend.API;

public class AppConfiguration
{
    public OrderSettings OrderSettings { get; }
    public OrderCleanupSettings OrderCleanupSettings { get; }
    public UserSettings UserSettings { get; }
    public CodeStorageSettings CodeStorageSettings { get; }
    public TokenGeneratorSettings TokenGeneratorSettings { get; }
    public TokenCleanupSettings TokenCleanupSettings { get; }

    public AppConfiguration(IConfiguration configuration)
    {
        OrderSettings = ConfigureOrder(configuration);
        OrderCleanupSettings = ConfigureOrderCleanup(configuration);
        UserSettings = ConfigureUser(configuration);
        CodeStorageSettings = ConfigureCodeStorage(configuration);
        TokenGeneratorSettings = ConfigureTokenGenerator(configuration);
        TokenCleanupSettings = ConfigureTokenCleanup(configuration);
    }

    private static TokenCleanupSettings ConfigureTokenCleanup(IConfiguration configuration)
    {
        int intervalFromMinutes = configuration.GetValue<int>("Configuration:TokenCleanupSettings:IntervalFromMinutes");
        int batchSize = configuration.GetValue<int>("Configuration:TokenCleanupSettings:BatchSize");

        TokenCleanupSettings settings = new()
        {
            Interval = TimeSpan.FromMinutes(intervalFromMinutes),
            BatchSize = batchSize
        };

        return settings;
    }

    private static OrderSettings ConfigureOrder(IConfiguration configuration)
    {
        int orderLifetime = configuration.GetValue<int>("Configuration:OrderSettings:OrderLifetimeFromMinutes");

        OrderSettings settings = new()
        {
            Lifetime = TimeSpan.FromMinutes(orderLifetime)
        };

        return settings;
    }

    private static OrderCleanupSettings ConfigureOrderCleanup(IConfiguration configuration)
    {
        int cleanupInterval = configuration.GetValue<int>("Configuration:OrderCleanupSettings:IntervalFromMinutes");
        int cleanupBatchSize = configuration.GetValue<int>("Configuration:OrderCleanupSettings:BatchSize");

        OrderCleanupSettings settings = new()
        {
            Interval = TimeSpan.FromMinutes(cleanupInterval),
            BatchSize = cleanupBatchSize
        };

        return settings;
    }

    private static UserSettings ConfigureUser(IConfiguration configuration)
    {
        int userSessionLifetime = configuration.GetValue<int>("Configuration:UserSettings:SessionLifetimeFromMinutes");
        UserSettings settings = new()
        {
            SessionLifetime = TimeSpan.FromMinutes(userSessionLifetime)
        };

        return settings;
    }

    private static CodeStorageSettings ConfigureCodeStorage(IConfiguration configuration)
    {
        int codeExpirationTime =
            configuration.GetValue<int>("Configuration:CodeStorageSettings:CodeExpirationTimeFromMinutes");
        CodeStorageSettings settings = new()
        {
            ExpirationTime = TimeSpan.FromMinutes(codeExpirationTime),
        };

        return settings;
    }

    private static TokenGeneratorSettings ConfigureTokenGenerator(IConfiguration configuration)
    {
        string path = "Configuration:Jwt";

        string secret = configuration.GetValue<string>($"{path}:Secret") ?? string.Empty;
        string issuer = configuration.GetValue<string>($"{path}:Issuer") ?? string.Empty;
        string audience = configuration.GetValue<string>($"{path}:Audience") ?? string.Empty;
        int accessExpirationMinutes = configuration.GetValue<int>($"{path}:AccessExpiresMinutes");
        int refreshExpiresDays = configuration.GetValue<int>($"{path}:RefreshExpiresDays");

        TokenGeneratorSettings settings = new()
        {
            Secret = secret,
            Issuer = issuer,
            Audience = audience,
            AccessTokenExpiration = TimeSpan.FromMinutes(accessExpirationMinutes),
            RefreshTokenExpiration = TimeSpan.FromDays(refreshExpiresDays),
        };

        return settings;
    }
}