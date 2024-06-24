namespace Users.Students.GetCourse;

public class Request
{
    public string StudentId { get; set; }
}

public class Response
{
    public CourseModel StudentCourse { get; set; }
}

public class CourseModel
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
