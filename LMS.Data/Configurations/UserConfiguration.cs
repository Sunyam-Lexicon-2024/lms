namespace LMS.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
       public void Configure(EntityTypeBuilder<User> builder)
       {
              builder.ToTable("Users")
                     .HasDiscriminator<string>("Role")
                     .HasValue<Student>("Student")
                     .HasValue<Teacher>("Teacher");
       }
}