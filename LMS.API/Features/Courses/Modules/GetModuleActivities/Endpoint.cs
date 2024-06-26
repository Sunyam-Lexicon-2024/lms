using Microsoft.AspNetCore.Http.HttpResults;

namespace Courses.Modules.GetModuleActivities;

public class Endpoint : Endpoint<Request, Results<Ok<Response>, NotFound, NoContent>, Mapper>
{

    public override void Configure()
    {
        Get("/courses/modules/{ModuleId}/get-all-courses");
    }

    public override async Task<Results<Ok<Response>, NotFound, NoContent>> ExecuteAsync(Request req, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        var module = await context.CourseElements.OfType<Module>()
                                        .Include(m => m.Activities)
                                        .Include(m => m.Parent)
                                        .FirstOrDefaultAsync(m => m.Id == req.ModuleId, ct);

        if (module is null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            if (!module.Activities.Any())
            {
                return TypedResults.NoContent();
            }
            else
            {

            }
            var activityModels = module.Activities.Select(Map.FromEntity);

            return TypedResults.Ok<Response>(new()
            {
                CourseId = module.Parent.Id,
                ModuleActivities = activityModels,
            });
        }
    }
}