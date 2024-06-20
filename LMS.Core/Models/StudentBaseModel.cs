namespace LMS.Core.Models;

public class StudentBaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? CourseName { get; set; }
    public int DocumentCount { get; set; }
}