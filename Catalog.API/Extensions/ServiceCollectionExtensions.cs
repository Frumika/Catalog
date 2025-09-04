using Catalog.Application.Interfaces;
using Catalog.Application.Services;
using Catalog.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, AppConfiguration config)
    {
        ConnectPostgres(services, config);
        AddCors(services);
        AddServices(services);

        services.AddControllers();

        return services;
    }

    private static void ConnectPostgres(IServiceCollection services, AppConfiguration config)
    {
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(config.GetConnectionString()));
    }

    private static void AddCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}