using Microsoft.AspNetCore.Http.HttpResults;

namespace Courses.Modules.Activities.CreateActivity;

public class Endpoint : Endpoint<
    Request,
    Results<Ok<Response>, BadRequest<string>>,
    Mapper>
{

    public override void Configure()
    {
        Post("/courses/modules/activities/create-activity");
    }

    public override async Task<
        Results<Ok<Response>,
        BadRequest<string>>>
        ExecuteAsync(Request req, CancellationToken ct)
    {

        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        bool activityExists = await context.CourseElements
                                        .OfType<ModuleActivity>()
                                        .AnyAsync(ce => ce.Name == req.Name && ce.ParentId == req.ParentId, ct);

        if (activityExists)
        {
            return TypedResults.BadRequest("Activity already registered");
        }

        var activityToCreate = Map.ToEntity(req);

        var createdActivity = await context.CourseElements.AddAsync(activityToCreate, ct);
        await context.SaveChangesAsync(ct);

        var activity = createdActivity.Entity as ModuleActivity;

        if (activity is not null)
        {
            return TypedResults.Ok(Map.FromEntity(activity));
        }

        return TypedResults.BadRequest("Could not create Entity ");

    }
}