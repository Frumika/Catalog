using Backend.API;
using Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = new AppConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddApplicationServices(configuration);

var app = builder.Build();
app.ApplyMigrations();
app.UseApplicationPipeline(configuration);
app.AddSwagger();
app.WarmupDatabase(configuration);

app.Run();