namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
    {

        if (env.IsDevelopment())
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
            options.UseSqlServer(config.GetConnectionString("Default")));
        // options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LmsDbContext;Trusted_Connection=True;MultipleActiveResultSets=true"));

        return services;
    }
}