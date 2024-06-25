namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
    {

        if (env.IsDevelopment() || env.EnvironmentName == "DevContainers")
        {
            services.AddFastEndpoints()
                    .SwaggerDocument(o =>
                {
                    o.DocumentSettings = s =>
                    {
                        s.Title = "Logistics Management System API";
                        s.Version = "v1";
                    };
                });
        }
        else
        {
            services.AddFastEndpoints();
        }

        services.AddDbContextFactory<LmsDbContext>(options =>
        {
            // set CONTAINER_ENV=true in your local environment to use a container/unix compatible connection string
            if (Environment.GetEnvironmentVariable("CONTAINER_ENV") is not null)
            {
                if (Environment.GetEnvironmentVariable("CONTAINER_ENV") is not null)
                {
                    options.UseSqlite(config.GetConnectionString("ContainerConnection"));
                }
                else
                {
                    options.UseSqlite(config.GetConnectionString("DefaultConnection"));
                }
            }
        });

        return services;
    }
}