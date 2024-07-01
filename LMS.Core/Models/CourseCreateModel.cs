using LMS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models;

public class CourseCreateModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}