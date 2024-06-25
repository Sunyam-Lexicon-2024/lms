namespace LMS.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
       public void Configure(EntityTypeBuilder<Course> builder)
       {
              builder.HasMany(c => c.Teachers)
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
       }
}