namespace Courses.CreateCourse;

public class Mapper : Mapper<CoursePostModel, CourseModel, Course>
{
    public override CoursePostModel ToEntity(Course c)
    {
        return new CoursePostModel()
        {
            Name = c.Name,
            StartDate = c.StartDate,
            EndDate = c.EndDate
        };
    }

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