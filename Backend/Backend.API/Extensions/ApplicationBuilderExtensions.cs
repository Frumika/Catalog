using Backend.DataAccess.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Backend.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void InitializeApplication(this WebApplication app)
    {
        app.ApplyMigrations()
            .WarmupDatabase();
    }
 
    public static WebApplication UseApplicationPipeline(this WebApplication app)
    {
        app.UseCors("AllowAllOrigins");
        app.MapControllers();
        app.MapGet("/api/health", () => Results.Ok());
        app.AddSwagger();

        return app;
    }

    private static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var mainDb = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        mainDb.Database.Migrate();

        return app;
    }

    private static WebApplication WarmupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        _ = dbContext.Users.Any();

        return app;
    }
    
    private static WebApplication AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}