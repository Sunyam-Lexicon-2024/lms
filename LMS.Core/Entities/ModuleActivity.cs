namespace LMS.Core.Entities;

public class ModuleActivity : CourseElement
{
    public ActivityType Type {get;set;}
}

public enum ActivityType {
    ELearning,
    Assignment,
    Lecture,
    Exercise
}