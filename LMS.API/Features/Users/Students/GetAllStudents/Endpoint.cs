using FastEndpoints;
using LMS.Data.DbContexts;
using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Users.Students.GetAllStudents;

public class Endpoint : EndpointWithoutRequest<Response, Mapper>
{

    public override void Configure()
    {
        Get("/users/students/get-all-students");
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