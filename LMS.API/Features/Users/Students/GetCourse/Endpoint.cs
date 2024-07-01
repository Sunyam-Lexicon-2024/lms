namespace Users.Students.GetCourse;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/users/students/{StudentId}/course");

        Description(d =>
         d.Produces<Response>(200, "application/json")
     );
        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Gets course for student";
            s.Description = "Gets the course for an authenticate student";
            s.ResponseExamples[200] = new StudentCourseBaseModel()
            {
                Name = "my course",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
            };
        });
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

        var course = await context.CourseElements.OfType<Course>()
                                                .FirstOrDefaultAsync(c => c.Id == student.CourseId, ct);

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
