namespace Courses.CreateCourse;

public class Mapper : ResponseMapper<CourseModel, Course>
{

    public override CourseModel FromEntity(Course c)
    {
        return new CourseModel()
        {
            CourseId = c.Id,
            Name = c.Name,
            StartDate = c.StartDate,
            EndDate = c.EndDate
        };
    }
}