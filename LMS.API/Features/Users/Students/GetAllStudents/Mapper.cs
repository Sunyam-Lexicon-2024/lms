namespace Users.Students.GetAllStudents;

public class Mapper : ResponseMapper<StudentBaseModel, Student>
{
    public override StudentBaseModel FromEntity(Student s)
    {
        return new StudentBaseModel()
        {
            Name = s.Name,
            Email = s.Email!,
            CourseName = s.Course?.Name ?? null,
            DocumentCount = s.Documents.Count,
        };
    }
}