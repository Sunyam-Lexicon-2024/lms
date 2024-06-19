namespace LMS.Core.Entities;

public enum Role
{
    Student,
    Teacher
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Role Role { get; set; }
    public ICollection<Course> Courses => [];
    public ICollection<Document> Documents => [];
}