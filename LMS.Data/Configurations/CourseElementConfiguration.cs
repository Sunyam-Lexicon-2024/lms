namespace LMS.Data.Configurations;

public class CourseElementConfiguration : IEntityTypeConfiguration<CourseElement>
{
    public void Configure(EntityTypeBuilder<CourseElement> builder)
    {
        builder.ToTable("CourseElements")
               .HasDiscriminator<string>("ElementType")
               .HasValue<Module>("Module")
               .HasValue<Course>("Course")
               .HasValue<ModuleActivity>("ModuleActivity");

        builder.HasOne(ce => ce.Parent)
               .WithMany(p => p.ChildElements)
               .HasForeignKey(ce => ce.ParentId)
               .IsRequired(false);

        builder.HasMany(ce => ce.ChildElements)
               .WithOne(p => p.Parent)
               .HasForeignKey(ce => ce.ParentId);
    }
}