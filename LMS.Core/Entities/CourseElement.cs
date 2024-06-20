namespace LMS.Core.Entities;

public abstract class CourseElement
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? ParentId { get; set; }
    public CourseElement? Parent { get; set; }
    public ICollection<CourseElement> ChildElements { get; } = [];
}