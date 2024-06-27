namespace Courses.Modules.Activities.CreateActivity;

public class Request
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ActivityType Type { get; set; }
    public int ParentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

}

public class Response
{
    public int ActivityId { get; set; }
    public string Name { get; set;}
}