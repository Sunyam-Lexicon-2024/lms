namespace LMS.Core.Entities;

public class Module : CourseElement
{
    public ICollection<Activity> Activities => ChildElements.OfType<Activity>().ToList();
}