namespace Courses.CreateCourse;

public class Mapper : Mapper<CoursePostModel, Response, Course>
{
    public override Course ToEntity(CoursePostModel c)
    {
        return new Course()
        {
            Name = c.Name,
            Description = c.Description,
            StartDate = c.StartDate,
            EndDate = c.EndDate
        };
    }
}