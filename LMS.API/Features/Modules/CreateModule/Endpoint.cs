namespace Modules.CreateModule;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("/modules");

        Description(d =>
         d.Produces<Response>(201, "application/json")
        );

        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Posts a module";
            s.Description = "Gets the course for an authenticate student";
            s.ResponseExamples[201] = "New module created";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        // check course exists
        var course = await context.CourseElements.FindAsync(req.CreateModuleBaseModel.ParentId);

        if (course is null)
        {
            ThrowError($"No course found for student");
            return; // Ensure method exits if there's an error
        }

        var newModule = new 
        {
            Id = Guid.NewGuid().ToString(),
            CourseId = req.CourseId,
            Name = req.Name,
            StartDate = req.StartDate,
            EndDate = req.EndDate
        };

        // Save the new module to database.
        await context.CourseElements.AddAsync(newModule);

        // Return the location header only
        var location = $"/courses/{req.CourseId}/modules";
        //Response.Headers.Add("Location", location);

        // Send 201 Created status with no body
        await SendCreatedAtAsync(location, null, cancellation: ct);

    }
}

