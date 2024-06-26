namespace Courses.GetCourseModules;

public class Request
{
    public int CourseId { get; set; }
}

public class Response
{
    public IEnumerable<ModuleModel> Modules { get; set; } = [];
}

public class ModuleModel
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}