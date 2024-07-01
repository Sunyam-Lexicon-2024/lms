using LMS.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.ConfigureApplication();

app.Run();
