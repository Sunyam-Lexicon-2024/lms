using FastEndpoints;
using LMS.Core.Entities;
using LMS.Core.Models;

namespace Users.Students.GetAllStudents;

public class Mapper : ResponseMapper<StudentBaseModel, Student>
{
    public override StudentBaseModel FromEntity(Student u)
    {
        return new StudentBaseModel()
        {
            Name = u.Name,
            Email = u.Email,
            CourseName = u.Course?.Name ?? null,
            DocumentCount = u.Documents.Count,
        };
    }
}