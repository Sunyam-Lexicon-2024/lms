using LMS.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.DbContexts;

public class LmsDbContext(DbContextOptions<LmsDbContext> options) :  IdentityDbContext<User>(options)
{

    public DbSet<Document> Documents => Set<Document>();
    public DbSet<CourseElement> CourseElements => Set<CourseElement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // extend parent configuration
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CourseElement>()
                    .ToTable("CourseElements")
                    .HasDiscriminator<string>("ElementType")
                    .HasValue<Course>("Course")
                    .HasValue<Module>("Module")
                    .HasValue<ModuleActivity>("ModuleActivity");

        modelBuilder.Entity<CourseElement>()
                    .HasOne(ce => ce.Parent)
                    .WithMany(p => p.ChildElements)
                    .HasForeignKey(ce => ce.ParentId)
                    .IsRequired(false);

        modelBuilder.Entity<CourseElement>()
                    .HasMany(ce => ce.ChildElements)
                    .WithOne(p => p.Parent)
                    .HasForeignKey(ce => ce.ParentId);

        modelBuilder.Entity<User>()
                    .ToTable("Users")
                    .HasDiscriminator<string>("Role")
                    .HasValue<Student>("Student")
                    .HasValue<Teacher>("Teacher");

        modelBuilder.Entity<Course>()
                    .HasMany(c => c.Teachers)
                    .WithMany(t => t.Courses)
                    .UsingEntity(
                        "CoursesTeachersJunction",
                        l => l.HasOne(typeof(Teacher))
                            .WithMany()
                            .HasForeignKey("CourseTeachersId")
                            .OnDelete(DeleteBehavior.Restrict),
                        r => r.HasOne(typeof(Course))
                            .WithMany()
                            .HasForeignKey("TeacherCoursesId")
                            .OnDelete(DeleteBehavior.Restrict),
                        j => j.HasKey("CourseTeachersId", "TeacherCoursesId")
                    );

        modelBuilder.Entity<Student>()
                    .HasOne(s => s.Course)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.CourseId);

        modelBuilder.Entity<ModuleActivity>()
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