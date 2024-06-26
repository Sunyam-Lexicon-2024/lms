namespace LMS.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
       public void Configure(EntityTypeBuilder<Student> builder)
       {
              builder.HasOne(s => s.Course)
                     .WithMany(c => c.Students)
                     .HasForeignKey(s => s.CourseId);
       }
}