using Catalog.API;
using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = new AppConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddApplicationServices(configuration);

var app = builder.Build();

app.UseApplicationPipeline(configuration);

app.Run();