using FastEndpoints;
using LMS.Data.DbContexts;
using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Users.Students.GetAllStudents;

namespace Users.Students.GetModuleDetails;



public class Endpoint : EndpointWithoutRequest<Response, Mapper>
{
    public override void Configure()
    {
        Get("/student/course/modules/{moduleId}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        var moduleId = Route<int>("moduleId");

        using var context = contextFactory.CreateDbContext();
        var module = await context.CourseElements
                                  .FirstOrDefaultAsync(m => m.Id == moduleId, ct);

        if (module is null)
        {
            ThrowError("Module not found");
        }

        var moduleModel = Map.FromEntity(module);

        await SendAsync(new Response
        {
            Module = moduleModel
        }, cancellation: ct);
    }
}