using Catalog.DataAccess.Contexts;

namespace Catalog.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationPipeline(this WebApplication app, AppConfiguration config)
    {
        // app.UseHttpsRedirection();
        app.UseCors("AllowAllOrigins");
        app.MapControllers();

        return app;
    }

    public static void WarmupDatabase(this WebApplication app, AppConfiguration config)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        dbContext.Users.Any();
    }
}