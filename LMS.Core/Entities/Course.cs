namespace LMS.Core.Entities;

public class Course : CourseElement
{
    public ICollection<Module> Modules => ChildElements.OfType<Module>().ToList();
}