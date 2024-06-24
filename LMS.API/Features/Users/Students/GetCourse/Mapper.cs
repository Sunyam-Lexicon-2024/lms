using Courses.GetAllCourses;

namespace Users.Students.GetCourse;

public class Mapper : ResponseMapper<CourseModel, CourseElement>
{
    public override CourseModel FromEntity(CourseElement ce)
    {
        return new CourseModel()
        {
            Name = ce.Name,
            StartDate = ce.StartDate,
            EndDate = ce.EndDate,
        };
    }
}