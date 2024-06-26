using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LMS.Data.DbContexts
{
    public class LmsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<LmsDbContext>
    {
        public LmsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddJsonFile("appsettings.Development.json")
                   .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();

            // set CONTAINER_ENV=true in your local environment to use a container/unix compatible connection string
            if (Environment.GetEnvironmentVariable("CONTAINER_ENV") is not null)
            {
                optionsBuilder.UseSqlite(configuration.GetConnectionString("ContainerConnection"));
            }
            else
            {
                optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }

            return new LmsDbContext(optionsBuilder.Options);
        }
    }
}