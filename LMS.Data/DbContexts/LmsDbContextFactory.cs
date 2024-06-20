using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lms.Data.DbContexts
{
    public class LmsDbContextFactory : IDesignTimeDbContextFactory<LmsDbContext>
    {
        public LmsDbContext CreateDbContext(string[] args)
        {

            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            IConfigurationRoot configuration;

            if (env is not null && env == "DevContainers")
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.DevContainers.json")
                    .Build();
            }
            else
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
            }

            var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();
            var connectionString = configuration.GetConnectionString("Default");

            optionsBuilder.UseSqlServer(connectionString);

            return new LmsDbContext(optionsBuilder.Options);
        }
    }
}