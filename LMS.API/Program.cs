using LMS.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApplication();

app.MapGet("/", () => "Hello World!");

app.Run();
