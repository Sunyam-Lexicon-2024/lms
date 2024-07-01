namespace Users.Teachers.GetCourses;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly IDbContextFactory<LmsDbContext> _contextFactory;

    public Endpoint(IDbContextFactory<LmsDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public override void Configure()
    {
        Get("/users/teachers/{UserId}/courses");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        using var context = _contextFactory.CreateDbContext();

        var teacher = await context.Users
            .OfType<Teacher>()
            .FirstOrDefaultAsync(t => t.Id == req.UserId, ct);

        if (teacher == null)
        {
            ThrowError($"Teacher with UserId: {req.UserId} not found");
            return;
        }

        var teacherCourses = await context.CourseElements
            .OfType<Course>()
            .Where(c => c.Teachers.Any(t => t.Id == req.UserId))
            .ToListAsync(ct);

        if (!teacherCourses.Any())
        {
            ThrowError($"No courses found for teacher with UserId: {req.UserId}");
            return;
        }

        var courseModels = teacherCourses.Select(Map.FromEntity).ToList();

        await SendAsync(new Response { TeacherCourses = courseModels }, cancellation: ct);
    }
}
