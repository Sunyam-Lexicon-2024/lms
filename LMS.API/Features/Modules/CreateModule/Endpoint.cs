using Microsoft.AspNetCore.Http.HttpResults;

namespace Modules.CreateModule;

public class Endpoint : Endpoint<
    Request,
    Results<Ok<Response>, BadRequest<string>>,
    Mapper>
{
    public override void Configure()
    {
        Post("/courses/modules/create-module");
        
        Description(d =>
         d.Produces<Response>(200, "application/json")
        );

        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Posts a module";
            s.Description = "Creates a new module";
            s.ExampleRequest = new Request()
            {
                Name = "My Module",
                Description = "My Module Description",
                ParentId = 2,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
            };
            s.ResponseExamples[200] = new Response() { ModuleId = 4, Name = "My Module" };
        });
    }

    public override async Task<Results<Ok<Response>,BadRequest<string>>> ExecuteAsync(Request req, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        // check course exists
        var course = await context.CourseElements.FindAsync(req.ParentId);

        if (course is null)
        {
            ThrowError($"No course found for student");
        }

        if (req.StartDate < course.StartDate || req.StartDate > course.EndDate)
        {
            AddError(r => r.StartDate, "Start date out of scope of course period");
        }

        if (req.EndDate > course.EndDate || req.EndDate < course.StartDate)
        {
            AddError(r => r.StartDate, "End date out of scope of course period");
        }

        var newModule = Map.ToEntity(req);

        // Save the new module to database.
        var createdModule = await context.CourseElements.AddAsync(newModule);
        await context.SaveChangesAsync(ct);

        var module = createdModule.Entity as Module;

        if (module is not null)
        {
            return TypedResults.Ok(Map.FromEntity(module));
        }

        return TypedResults.BadRequest("Could not create the Module ");
    }
}
