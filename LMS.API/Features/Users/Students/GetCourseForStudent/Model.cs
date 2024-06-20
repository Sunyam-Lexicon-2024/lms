using FastEndpoints;

namespace Users.Students.GetcourseForStudent;

public class Request
{
    public int CourseId { get; set; }
}


public class Response
{
    public string CourseName { get; set; } = default!;
}


