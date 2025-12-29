using Backend.DataAccess.Postgres.Contexts;
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
        var mainDb = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        mainDb.Database.Migrate();
    }

    public static void WarmupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
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