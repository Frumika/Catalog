using Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.ApplyMigrations();
app.UseApplicationPipeline();
app.AddSwagger();
app.WarmupDatabase();

app.Run();