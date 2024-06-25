namespace LMS.Data.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
       public void Configure(EntityTypeBuilder<Document> builder)
       {
                builder.HasOne(d => d.User)
                       .WithMany()
                       .HasForeignKey(d => d.UploaderId)
                       .OnDelete(DeleteBehavior.Restrict);
       }
}