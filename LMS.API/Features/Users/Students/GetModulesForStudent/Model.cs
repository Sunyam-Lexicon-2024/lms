namespace Users.Students.GetModulesForStudent;

public class Request
{
    public int StudentId { get; set; }
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