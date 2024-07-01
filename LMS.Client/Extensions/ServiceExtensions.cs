using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using LMS.Client.Components.Account;
using LMS.Data.DbContexts;
using LMS.Client.Stores;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace LMS.Client.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services.AddBlazorBootstrap();

        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        services.AddDbContext<LmsDbContext>(options =>
        {
            // set CONTAINER_ENV=true in your local environment to use a container/unix compatible connection string
            if (Environment.GetEnvironmentVariable("CONTAINER_ENV") is not null)
            {
                options.UseSqlite(configuration.GetConnectionString("ContainerConnection"));
            }
            else
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }
        });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddUserStore<LmsUserStore>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<LmsDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

        services.AddScoped(sp =>
            new HttpClient()
            {
                BaseAddress = new Uri(configuration["API:BaseAddress"]!)
            }
        );

        return services;
    }
}