using System.Text.Json.Serialization;
using Backend.API.Background;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Auth;
using Backend.Application.Services.Carts;
using Backend.Application.Services.Catalog;
using Backend.Application.Services.Orders;
using Backend.Application.Services.PickupPoints;
using Backend.Application.Services.Reviews;
using Backend.Application.Services.Wishlists;
using Backend.Domain.Interfaces;
using Backend.Domain.Settings;
using Backend.Infrastructure.Services.Background;
using Backend.Infrastructure.Services.Notifications;
using Backend.Infrastructure.Services.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Resend;
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
            .UseResend(config)
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
        string? connectionString = config["Databases:Redis:Main"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Redis connection string is missing in configuration.");

        IConnectionMultiplexer connection = ConnectionMultiplexer.Connect(connectionString);

        services.AddSingleton(connection);
        services.AddScoped<ICodeStorage, RedisCodeStorage>();

        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
    {
        AppConfiguration appConfiguration = new(config);

        services.AddSingleton<OrderSettings>(_ => appConfiguration.OrderSettings);
        services.AddSingleton<OrderCleanupSettings>(_ => appConfiguration.OrderCleanupSettings);
        services.AddSingleton<UserSettings>(_ => appConfiguration.UserSettings);
        services.AddSingleton<CodeStorageSettings>(_ => appConfiguration.CodeStorageSettings);

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
        services.AddScoped<ReviewService>();
        services.AddScoped<PickupPointService>();

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

    private static IServiceCollection UseResend(this IServiceCollection services, IConfiguration config)
    {
        var apiToken = config["Resend:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiToken))
        {
            throw new InvalidOperationException("Resend API token is missing in configuration.");
        }

        services.AddResend(options => options.ApiToken = apiToken);
        services.AddScoped<IVerificationSender, ResendEmailSender>();

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
            options.AddServer(
                new OpenApiServer
                {
                    Url = "http://localhost:8000",
                    Description = "Reverse proxy server"
                }
            );
        });
        return services;
    }
}