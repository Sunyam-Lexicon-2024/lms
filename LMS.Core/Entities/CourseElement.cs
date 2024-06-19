namespace LMS.Core.Entities;

public abstract class CourseElement
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int? ParentId { get; set; }
    public CourseElement? Parent { get; set; }
    public ICollection<CourseElement> ChildElements  => [];
}