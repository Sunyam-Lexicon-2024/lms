using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.DbContexts;

public class LmsDbContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    public DbSet<CourseElement> CourseElements => Set<CourseElement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseElement>()
                    .ToTable("CourseElements")
                    .HasDiscriminator<string>("ElementType")
                    .HasValue<Course>("Course")
                    .HasValue<Module>("Module")
                    .HasValue<Activity>("Activity");

        modelBuilder.Entity<CourseElement>()
                    .HasOne(ce => ce.Parent)
                    .WithMany(p => p.ChildElements)
                    .HasForeignKey(ce => ce.ParentId)
                    .IsRequired(false);

        modelBuilder.Entity<CourseElement>()
                    .HasMany(ce => ce.ChildElements)
                    .WithOne(p => p.Parent)
                    .HasForeignKey(ce => ce.ParentId);

        modelBuilder.Entity<Activity>()
                    .Property(a => a.Type)
                    .HasConversion<string>();
    }
}