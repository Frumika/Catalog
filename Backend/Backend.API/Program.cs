using Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.InitializeApplication();
app.UseApplicationPipeline();

app.Run();