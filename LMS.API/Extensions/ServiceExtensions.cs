namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddFastEndpoints();

        services.AddDbContextFactory<LmsDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        return services;
    }
}