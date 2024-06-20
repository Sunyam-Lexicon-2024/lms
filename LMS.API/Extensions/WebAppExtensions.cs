using FastEndpoints;
using Lms.Data.DbContexts;
using LMS.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Extensions;

public static class WebAppExtensions
{
    public static async Task<WebApplication> ConfigureApplication(this WebApplication app)
    {
        app.UseDefaultExceptionHandler()
           .UseFastEndpoints(config =>
           {
               config.Endpoints.RoutePrefix = "api"; // prefix all routes with "/api"
               config.Endpoints.Configurator = ep =>
               {
                   ep.AllowAnonymous(); // disable auth temporarily
               };
           });

        if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "DevContainers")
        {

            app.UseDeveloperExceptionPage();

            if (Environment.GetEnvironmentVariable("SEED_DATA") == "1")
            {
                await app.SeedDataAsync();
            }
        }

        return app;
    }

    private static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = app.ApplicationServices.GetService<ILogger<Program>>() ?? throw new ArgumentException("Could not acquire Logger from Service Collection");
        var serviceProvider = scope.ServiceProvider;
        var LmsDbContext = serviceProvider.GetRequiredService<LmsDbContext>();

        BaseSeeds baseSeeds = new(LmsDbContext);

        await LmsDbContext.Database.EnsureDeletedAsync();
        await LmsDbContext.Database.MigrateAsync();

        try
        {
            await baseSeeds.InitAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", new { ex.Message, ex.StackTrace });
            throw;
        }
    }
}