using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.DbContexts;

public class LmsDbContext(DbContextOptions<LmsDbContext> options) : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
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

        modelBuilder.Entity<Course>()
                    .HasMany(e => e.Users)
                    .WithMany(e => e.Courses)
                    .UsingEntity("CoursesUsersJunction");

        modelBuilder.Entity<Activity>()
                    .Property(a => a.Type)
                    .HasConversion<string>();

        modelBuilder.Entity<Document>()
                    .HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UploaderId)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
                    .HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(d => d.CommenterId)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}