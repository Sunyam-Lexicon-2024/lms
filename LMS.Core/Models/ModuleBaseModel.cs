namespace LMS.Core.Models;

public class ModuleBaseModel
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}