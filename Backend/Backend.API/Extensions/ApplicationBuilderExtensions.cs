using Backend.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationPipeline(this WebApplication app)
    {
        app.UseCors("AllowAllOrigins");
        app.MapControllers();

        return app;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var productsDb = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        var usersDb = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        productsDb.Database.Migrate();
        usersDb.Database.Migrate();
    }

    public static void WarmupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        _ = dbContext.Users.Any();
    }
    
    public static void AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}