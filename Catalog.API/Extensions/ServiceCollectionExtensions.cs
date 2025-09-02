using Catalog.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, AppConfiguration config)
    {
        services.ConnectPostgres(config);
        AddCors(services);

        return services;
    }

    private static void ConnectPostgres(this IServiceCollection services, AppConfiguration config)
    {
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(config.GetConnectionString()));
    }

    private static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
}