namespace LMS.Core.Entities;

public class Student : User
{
    public Course? Course { get; set; }
    public int? CourseId { get; set; }
}