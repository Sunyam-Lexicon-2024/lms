namespace Courses.GetAllCourses;

public class Endpoint : EndpointWithoutRequest<IEnumerable<CourseModel>, Mapper>
{

    public override void Configure()
    {
        Get("/courses/get-all-courses");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<Course> courses;
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();
        courses = await context.CourseElements.OfType<Course>()
                                               .ToListAsync(ct);

        var courseModels = courses.Select(Map.FromEntity).ToList();

        await SendAsync(courseModels, cancellation: ct);
    }
}