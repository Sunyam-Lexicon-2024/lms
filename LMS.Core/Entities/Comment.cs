namespace LMS.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public User User { get; set; } = null!;
    public int CommenterId { get; set; }
    public Document Document { get; set; } = null!;
    public int DocumentId { get; set; }
}