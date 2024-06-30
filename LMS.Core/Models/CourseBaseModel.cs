namespace LMS.Core.Models;

public class CourseBaseModel
{
    //public int? CourseId { get; set; } = default!;
    public int CourseId { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public string? Description { get; set; } = default!;

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}