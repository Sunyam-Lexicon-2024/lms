namespace LMS.Core.Models;

public class TeacherCourseBaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
