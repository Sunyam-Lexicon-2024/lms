

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

        using var context = await contextFactory.CreateDbContextAsync(ct);

        Student? student = await context.Users.OfType<Student>()
                                            .FirstOrDefaultAsync(
                                                u => u.Id == req.StudentId, ct);

        Course? course = student.Course;
        var modules = course.Modules;

        await SendAsync(new()
        {
            Modules = modules
        }, cancellation: ct);

    }
}