namespace Catalog.API;

public class AppConfiguration
{
    private const string ConnectionString = "Host=localhost;Database=Users;Username=postgres;Password=1234";

    public IConfiguration Configuration { get; set; }
    public IWebHostEnvironment Environment { get; set; }

    public AppConfiguration(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public string? GetConnectionString(string name) => Configuration.GetConnectionString(name);
}