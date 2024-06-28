using LMS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models;

public class CourseCreateModel
{
    [Required]
    [MinLength(3, ErrorMessage = "The name must be at least 3 charactrs to be identifiable")]
    public string Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Required]
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}