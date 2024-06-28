namespace Courses.GetCourse;

public class Request
{
    public int CourseId { get; set; }
}

public class Response
{
    public CourseResponseModel StudentCourse { get; set; }
}

public class CourseResponseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
