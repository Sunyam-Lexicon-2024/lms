namespace Courses.GetCourseModules;

public class Request
{
    public int CourseId { get; set; }
}

public class Response
{
    public IEnumerable<ModuleBaseModel> Modules { get; set; } = [];
}