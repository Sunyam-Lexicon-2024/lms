namespace LMS.Core.Entities;

public class Module : CourseElement
{
    public ICollection<ModuleActivity> Activities => ChildElements.OfType<ModuleActivity>().ToList();
}