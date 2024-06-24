namespace LMS.Data.Configurations;

public class ModuleActivityConfiguration : IEntityTypeConfiguration<ModuleActivity>
{
       public void Configure(EntityTypeBuilder<ModuleActivity> builder)
       {
              builder.Property(a => a.Type)
                     .HasConversion<string>();
       }
}