namespace Courses.GetAllCourses;

public class Endpoint : EndpointWithoutRequest<IEnumerable<Response>, Mapper>
{

    public override void Configure()
    {
        Get("/courses/get-all-courses");
        Description(d =>
          d.Produces<IEnumerable<Response>>(200, "application/json")
      );
        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Gets all courses";
            s.Description = "Gets all courses registered in the LMS database";
            s.ResponseExamples[200] = new List<Response>()
            {
                    new()
                    {
                        CourseId = 1,
                        Name = "my course",
                        Description = "Description of course 1"
                    },
                    new()
                    {
                        CourseId = 2,
                        Name = "my second course",
                        Description = "Description of course 2"
                    }
            };
        });
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