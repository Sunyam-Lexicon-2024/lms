using FastEndpoints;

namespace Users.Students.GetcourseForStudent;

public class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/users/students/get-course-for-student");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await SendAsync(new()
        {
            CourseName = "placeholder course name"
        }, cancellation: ct);
    }
}