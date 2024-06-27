namespace Courses.CreateCourse;

public class CoursePostModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

public class Response
{
    public string Message { get; set; }
}