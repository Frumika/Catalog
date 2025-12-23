namespace Backend.API;

public class AppConfiguration
{
    public IConfiguration Configuration { get; set; }
    public IWebHostEnvironment Environment { get; set; }

    public AppConfiguration(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public string? GetConnectionString(string name) => Configuration.GetConnectionString(name);
}