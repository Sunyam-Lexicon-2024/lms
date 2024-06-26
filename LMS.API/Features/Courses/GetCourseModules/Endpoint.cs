namespace Courses.GetCourseModules;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/courses/{CourseId}/modules");
        // Swagger description
        Description(d =>
            d.Produces<Response>(200, "application/json")
        );
        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Gets all modules for course.";
            s.Description = "Gets all modules for a specific course (lookup on the course ID).";
            s.ExampleRequest = new Request() { CourseId = 1 };
            s.ResponseExamples[200] = new Response()
            {
                Modules = [
                    new()
                    {
                        Name = "my module",
                        StartDate = DateOnly.FromDateTime(DateTime.Today),
                        EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(15))
                    },
                    new()
                    {
                        Name = "my second module",
                        StartDate = DateOnly.FromDateTime(DateTime.Today),
                        EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30))
                    }
                ]
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
        var course = await context.CourseElements.OfType<Course>()
                                        .Include(c => c.Modules)
                                        .FirstOrDefaultAsync(c => c.Id == req.CourseId, ct);

        var modules = course.Modules;

        IEnumerable<ModuleBaseModel> moduleModels = [];

        if (modules is not null)
        {
            moduleModels = modules.Select(Map.FromEntity).ToList();
        }

        await SendAsync(new()
        {
            Modules = moduleModels
        }, cancellation: ct);
    }
}