namespace Courses.GetAllCourses;

public class Mapper : ResponseMapper<Response, Course>
{

    public override Response FromEntity(Course c)
    {
        return new Response()
        {
            CourseId = c.Id,
            Name = c.Name,
            Description = c.Description,
            StartDate = c.StartDate,
            EndDate = c.EndDate
        };
    }
}