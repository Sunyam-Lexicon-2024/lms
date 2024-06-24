namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddFastEndpoints();

        services.AddDbContextFactory<LmsDbContext>(options =>
        {
            if (Environment.GetEnvironmentVariable("CONTAINER_ENV") is not null)
            {
                options.UseSqlite(config.GetConnectionString("ContainerConnection"));
            }
            else
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            }
        });

        return services;
    }
}