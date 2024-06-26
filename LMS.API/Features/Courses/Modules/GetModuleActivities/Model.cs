namespace Courses.Modules.GetModuleActivities;

public class Request
{
    public int ModuleId { get; set; }
}

public class Response
{
    public int? CourseId { get; set; }
    public IEnumerable<ModuleActivityBaseModel> ModuleActivities {get; set;}
}