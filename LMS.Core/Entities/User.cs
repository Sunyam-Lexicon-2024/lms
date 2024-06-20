namespace LMS.Core.Entities;

public abstract class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Document> Documents { get; } = [];
}