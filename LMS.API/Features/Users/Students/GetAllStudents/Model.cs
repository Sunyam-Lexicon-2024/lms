using LMS.Core.Models;

namespace Users.Students.GetAllStudents;

public class Response
{
    public IEnumerable<StudentBaseModel> Students { get; set; }
}