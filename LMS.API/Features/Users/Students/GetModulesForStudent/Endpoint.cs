namespace Users.Students.GetModulesForStudent;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/users/students/{StudentId}/modules");
        // Swagger description
        Description(d =>
            d.Produces<Response>(200, "application/json")
        );
        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Gets all modules for student.";
            s.Description = "Gets all modules for a specific student (lookup on the students ID).";
            s.ExampleRequest = new Request() { StudentId = 1 };
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
        var student = await context.Users.OfType<Student>()
                                        .FirstOrDefaultAsync(s => s.Id == req.StudentId, ct);

        var course = await context.CourseElements.OfType<Course>()
                                                .Include(c => c.Modules)
                                                .FirstOrDefaultAsync(ce => ce.Id == student.CourseId, ct);

        var modules = course.Modules;

        IEnumerable<ModuleModel> moduleModels = [];

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