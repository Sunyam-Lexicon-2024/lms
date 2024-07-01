namespace Users.Teachers.GetCourses;

public class Mapper : ResponseMapper<TeacherCourseBaseModel, Course>
{
    public override TeacherCourseBaseModel FromEntity(Course c)
    {
        return new TeacherCourseBaseModel()
        {
            Name = c.Name,
            Description = c.Name,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
        };
    }
}