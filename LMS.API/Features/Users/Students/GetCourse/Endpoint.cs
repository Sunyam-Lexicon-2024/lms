namespace Users.Students.GetCourse;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/users/students/{StudentId}/course");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();


        var student = await context.Users.OfType<Student>()
            .FirstOrDefaultAsync(s => s.Id == req.StudentId, ct);

        //var course = await context.CourseElements.OfType<Course>()
        //.FirstOrDefaultAsync(ce => ce.Id == student.CourseId, ct);

        var course = await context.CourseElements.FindAsync(student.CourseId);

        if (course is null)
        {
            ThrowError($"No course found for student");
            return; // Ensure method exits if there's an error
        }

        var studentCourse = Map.FromEntity(course);

        await SendAsync(new()
        {
            StudentCourse = studentCourse
        }, cancellation: ct);
    }
}
