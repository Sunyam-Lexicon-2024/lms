using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LMS.Data.DbContexts
{
    public class LmsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<LmsDbContext>
    {
        public LmsDbContext CreateDbContext(string[] args)
        {

            string? env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var configurationBuilder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory());

            if (env is not null && env == "DevContainers")
            {
                configurationBuilder
                   .AddJsonFile("appsettings.DevContainers.json");
            }
            else
            {
                configurationBuilder
                   .AddJsonFile("appsettings.json")
                   .AddJsonFile("appsettings.Development.json");
            }

            var configuration = configurationBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);

            return new LmsDbContext(optionsBuilder.Options);
        }
    }
}