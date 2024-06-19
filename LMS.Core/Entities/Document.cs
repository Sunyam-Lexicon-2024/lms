namespace LMS.Core.Entities;

public class Document
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime UploadedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}