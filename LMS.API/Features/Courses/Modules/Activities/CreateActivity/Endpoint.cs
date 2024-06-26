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
        Description(d =>
        {
            d.Produces<Response>(200, "application/json");
            d.Produces(403);
        });
        Summary(s =>
        {
            s.Summary = "Creates Module Activity.";
            s.Description = "Creates a new module activity by the specified values in the request ";
            s.ExampleRequest = new Request()
            {
                Name = "My Module Activity",
                Description = "My Module Activity Description",
                ParentId = 10,
                Type = ActivityType.ELearning,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
            };
            s.ResponseExamples[200] = new Response() { ActivityId = 1, Name = "My Module" };
        });
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
                                        .AnyAsync(ce => ce.Name == req.Name
                                            && ce.Type == req.Type
                                            && ce.ParentId == req.ParentId, ct);

        if (activityExists)
        {
            AddError(r => r.GetType().Name, "Activity exists");
        }

        var module = await context.CourseElements.OfType<Module>().FirstOrDefaultAsync(m => m.Id == req.ParentId, ct);

        if (req.StartDate < module.StartDate || req.StartDate > module.EndDate)
        {
            AddError(r => r.StartDate, "Start date out of scope of module period");
        }

        if (req.EndDate > module.EndDate || req.EndDate < module.StartDate)
        {
            AddError(r => r.StartDate, "End date out of scope of module period");
        }

        ThrowIfAnyErrors();

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