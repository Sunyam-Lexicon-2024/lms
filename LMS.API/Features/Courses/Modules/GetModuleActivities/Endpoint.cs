using Microsoft.AspNetCore.Http.HttpResults;

namespace Courses.Modules.GetModuleActivities;

public class Endpoint : Endpoint<
    Request,
    Results<Ok<IEnumerable<ModuleActivityBaseModel>>, NotFound, NoContent>,
    Mapper>
{

    public override void Configure()
    {
        Get("/courses/modules/{ModuleId}/get-all-activities");
        Description(d =>
          d.Produces<IEnumerable<ModuleActivityBaseModel>>(200, "application/json")
      );
        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Gets all module activities.";
            s.Description = "Gets all actitvities for a specific module (lookup on the module ID).";
            s.ExampleRequest = new Request() { ModuleId = 1 };
            s.ResponseExamples[200] = new List<ModuleActivityBaseModel> {
                    new()
                    {
                        Name = "My module",
                        Description = "First module description",
                        StartDate = DateOnly.FromDateTime(DateTime.Today),
                        EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(15))
                    },
                    new()
                    {
                        Name = "My second module",
                        Description = "Second module description",
                        StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(15)),
                        EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30))
                    }
            };
        });
    }

    public override async Task<
        Results<Ok<IEnumerable<ModuleActivityBaseModel>>,
        NotFound,
        NoContent>>
        ExecuteAsync(Request req, CancellationToken ct)
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

        if (module.Activities.Count == 0)
        {
            return TypedResults.NoContent();
        }

        var activityModels = module.Activities.Select(Map.FromEntity);

        return TypedResults.Ok(activityModels);
    }
}