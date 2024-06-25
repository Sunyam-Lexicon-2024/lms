namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddFastEndpoints();

        services.AddDbContextFactory<LmsDbContext>(options =>
        {
            // set CONTAINER_ENV=true in your local environment to use a container/unix compatible connection string
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