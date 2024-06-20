namespace LMS.API.Extensions;

public static class ServiceExtensions
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddFastEndpoints();

        services.AddDbContextFactory<LmsDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LmsDbContext;Trusted_Connection=True;MultipleActiveResultSets=true"));
            // options.UseSqlServer(config.GetConnectionString("Default")));

        return services;
    }
}