namespace Courses.GetCourse;

public class Mapper : ResponseMapper<CourseResponseModel, Course>
{
    public override CourseResponseModel FromEntity(Course c)
    {
        return new CourseResponseModel()
        {
            Name = c.Name,
            Description = c.Name,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
        };
    }
}