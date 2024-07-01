namespace Users.Teachers.GetCourses;

public class Request
{
    public string UserId { get; set; }
}

public class Response
{
    public IEnumerable<TeacherCourseBaseModel> TeacherCourses { get; set; }
}

// public class TeacherCourseBaseModel
// {
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public DateOnly StartDate { get; set; }
//     public DateOnly EndDate { get; set; }
// }
