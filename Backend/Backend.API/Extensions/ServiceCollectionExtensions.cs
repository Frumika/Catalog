using System.Text.Json.Serialization;
using Backend.Application.Interfaces;
using Backend.Application.Services;
using Backend.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .ConnectPostgres(config)
            .AddCorsPolicy()
            .AddApplicationServices()
            .AddApplicationControllers()
            .AddSwaggerGen();

        return services;
    }

    private static IServiceCollection ConnectPostgres(this IServiceCollection services, IConfiguration config)
    {
        string? connectionString = config["Databases:Main"];
        services.AddDbContext<MainDbContext>(options => options.UseNpgsql(connectionString));
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
        services.AddScoped<IIdentityService, IdentityService>();
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