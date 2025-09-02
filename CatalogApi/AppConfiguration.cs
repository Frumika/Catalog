namespace CatalogApi;

public class AppConfiguration
{
    public IConfiguration Configuration { get; set; }
    public IWebHostEnvironment Environment { get; set; }

    public AppConfiguration(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public string GetConnectionString(string name)
    {
        return Configuration.GetConnectionString(name) ?? string.Empty;
    }

}