namespace LMS.Core.Entities;

public class Course : CourseElement
{
    public ICollection<Module> Modules => ChildElements.OfType<Module>().ToList();
    public ICollection<Student> Students { get; } = [];
    public ICollection<Teacher> Teachers { get; } = [];
}