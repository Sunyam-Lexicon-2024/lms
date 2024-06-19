namespace LMS.Core.Entities;

public class Activity : CourseElement
{
    public ActivityType Type {get;set;}
}

public enum ActivityType {
    ELearning,
    Assignment,
    Lecture,
    Exercise
}