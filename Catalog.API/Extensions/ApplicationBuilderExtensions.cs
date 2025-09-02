namespace Catalog.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationPipeline(this WebApplication app, AppConfiguration config)
    {
        app.UseHttpsRedirection();
        app.UseCors("AllowAllOrigins");
        app.MapControllers();

        return app;
    }
}