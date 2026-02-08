using System.Text.Json.Serialization;
using Backend.API.Background;
using Backend.Application.Services;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Redis;
using Backend.DataAccess.Storages;
using Backend.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;


namespace Backend.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .ConnectPostgres(config)
            .ConnectRedis(config)
            .AddSettings(config)
            .AddApplicationServices()
            .AddApplicationControllers()
            .AddCorsPolicy()
            .AddSwagger();

        return services;
    }

    private static IServiceCollection ConnectPostgres(this IServiceCollection services, IConfiguration config)
    {
        string? connectionString = config["Databases:Postgres:Main"];
        services.AddDbContext<MainDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection ConnectRedis(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config["Databases:Redis:Main"] ?? string.Empty;
        IConnectionMultiplexer connection = ConnectionMultiplexer.Connect(connectionString);

        services.AddSingleton(new RedisDbProvider(connection));
        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
    {
        AppConfiguration appConfiguration = new(config);

        services.AddSingleton<OrderSettings>(_ => appConfiguration.OrderSettings);
        services.AddSingleton<OrderCleanupSettings>(_ => appConfiguration.OrderCleanupSettings);
        services.AddSingleton<UserSettings>(_ => appConfiguration.UserSettings);

        return services;
    }

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CatalogService>();
        services.AddScoped<AuthService>();
        services.AddScoped<CartService>();
        services.AddScoped<WishlistService>();
        services.AddScoped<OrderService>();
        services.AddScoped<OrdersCleanupService>();

        services.AddScoped<UserSessionStorage>();
        services.AddScoped<OrderIndexStorage>();

        services.AddHostedService<OrdersCleanupBackgroundService>();

        return services;
    }

    private static IServiceCollection AddApplicationControllers(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.FullName); });
        return services;
    }
}