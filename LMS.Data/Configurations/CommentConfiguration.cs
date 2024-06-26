namespace LMS.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
       public void Configure(EntityTypeBuilder<Comment> builder)
       {
              builder.HasOne(c => c.User)
                     .WithMany()
                     .HasForeignKey(d => d.CommenterId)
                     .OnDelete(DeleteBehavior.Restrict);
       }
}