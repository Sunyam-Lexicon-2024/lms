using LMS.Client.Components;
using LMS.Data.DbContexts;
using LMS.Data.Seeds;

namespace LMS.Client.Extensions;

public static class WebAppExtensions
{
    public static async Task<WebApplication> ConfigureApplication(this WebApplication app)
    {
        var logger = app.Services.GetService<ILogger<Program>>() ?? throw new ArgumentException("Could not acquire Logger from Service Collection");
        try
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapAdditionalIdentityEndpoints();

            if (Environment.GetEnvironmentVariable("SEED_DATA") == "1")
            {
                await app.SeedDataAsync();
            }

            return app;
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", new { ex.Message, ex.StackTrace });
            throw;
        }
    }

    private static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var lmsDbContext = serviceProvider.GetRequiredService<LmsDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var userStore = serviceProvider.GetRequiredService<IUserStore<User>>();
        var logger = serviceProvider.GetRequiredService<ILogger<BaseSeeds>>();

        BaseSeeds baseSeeds = new(lmsDbContext, userManager, userStore, logger);

        await lmsDbContext.Database.EnsureDeletedAsync();
        await lmsDbContext.Database.MigrateAsync();
        await app.SeedIdentityRoles();
        await baseSeeds.InitAsync();

    }
}