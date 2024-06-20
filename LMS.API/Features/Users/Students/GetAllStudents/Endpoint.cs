namespace Users.Students.GetAllStudents;

public class Endpoint : EndpointWithoutRequest<Response, Mapper>
{

    public override void Configure()
    {
        Get("/users/students/get-all-students");
        Description(d =>
          d.Produces<Response>(200, "application/json")
      );
        Summary(s =>
        {
            s.Summary = "Gets all registered students.";
            s.Description = "Gets all modules for a specific student (lookup on the students ID).";
            s.ResponseExamples[200] = new Response()
            {
                Students =
                [
                    new() { Name = "John Doe", Email = "j.doe@some.domain", CourseName = "A school Course", DocumentCount = 50},
                    new() { Name = "Rick Hunter", Email = "r.h@some.domain",  CourseName = "Another school course", DocumentCount = 25},
                    new() { Name = "Hiroshi Kamiya",  Email = "kami-god@some.domain", CourseName = "Yet another school Course", DocumentCount = 31},
                ]
            };
        });

    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<Student> students;
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();
        students = await context.Users.OfType<Student>()
                                    .Include(s => s.Course)
                                    .ToListAsync(ct);

        var studentModels = students.Select(Map.FromEntity).ToList();

        await SendAsync(new()
        {
            Students = studentModels
        }, cancellation: ct);
    }
}