using Courses.GetAllCourses;

namespace Users.Students.GetCourse;

public class Mapper : ResponseMapper<StudentCourseBaseModel, CourseElement>
{
    public override StudentCourseBaseModel FromEntity(CourseElement ce)
    {
        return new StudentCourseBaseModel()
        {
            Name = ce.Name
            //StartDate = ce.StartDate,
            //EndDate = ce.EndDate,
        };
    }
}