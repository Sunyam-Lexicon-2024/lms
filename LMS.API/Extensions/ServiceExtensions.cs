using FastEndpoints;
using Lms.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddFastEndpoints();
        services.AddDbContext<LmsDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        services.AddDbContextFactory<LmsDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        return services;
    }
}