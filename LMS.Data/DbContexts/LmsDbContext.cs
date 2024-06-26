using LMS.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.DbContexts;

public class LmsDbContext(DbContextOptions<LmsDbContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<Document> Documents => Set<Document>();
    public DbSet<CourseElement> CourseElements => Set<CourseElement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // extend parent configuration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LmsDbContext).Assembly);
    }
}