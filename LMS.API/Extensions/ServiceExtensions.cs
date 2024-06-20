namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddFastEndpoints();

        services.AddDbContextFactory<LmsDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));
            // options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LmsDbContext;Trusted_Connection=True;MultipleActiveResultSets=true"));

        return services;
    }
}