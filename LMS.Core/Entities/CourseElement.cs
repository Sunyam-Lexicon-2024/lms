namespace LMS.Core.Entities;

public abstract class CourseElement
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateOnly StartDate { get; set; }
    public required DateOnly EndDate { get; set; }
    public int? ParentId { get; set; }
    public CourseElement? Parent { get; set; }
    public ICollection<CourseElement> ChildElements { get; } = [];
}