namespace Users.Students.GetModulesForStudent;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/users/students/get-modules-for-student");
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

        var modules = student.Course.Modules;

        var moduleModels = modules.Select(Map.FromEntity);

        await SendAsync(new()
        {
            Modules = moduleModels
        }, cancellation: ct);
    }
}