namespace LMS.Core.Models;

public class ModuleActivityCreateModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int ParentId { get; set; }
    public ActivityType Type { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}