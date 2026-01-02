using System.Text.Json.Serialization;
using Backend.Application.Services;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Redis;
using Backend.DataAccess.Storages;
using Backend.DataAccess.Storages.Interfaces;
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
            .AddCorsPolicy()
            .AddApplicationServices()
            .AddApplicationControllers()
            .AddSwaggerGen();

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
        string userConnectionString = config["Databases:Redis:UserSessions"] ?? string.Empty;
        string cartConnectionString = config["Databases:Redis:CartSessions"] ?? string.Empty;

        IConnectionMultiplexer userConnection = ConnectionMultiplexer.Connect(userConnectionString);
        IConnectionMultiplexer cartConnection = ConnectionMultiplexer.Connect(cartConnectionString);

        services.AddSingleton(new RedisDbProvider(userConnection, cartConnection));

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
        services.AddScoped<ICatalogService, CatalogService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserSessionStorage, UserSessionStorage>();

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
}