namespace Users.Students.GetCourse;

public class Mapper : ResponseMapper<StudentCourseBaseModel, Course>
{
    public override StudentCourseBaseModel FromEntity(Course c)
    {
        return new StudentCourseBaseModel()
        {
            Name = c.Name,
            Description = c.Name,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
        };
    }
}