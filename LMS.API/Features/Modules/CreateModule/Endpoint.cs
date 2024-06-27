namespace Modules.CreateModule;

public class Endpoint : Endpoint<CreateModuleModel, Response, Mapper>
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

    public override async Task HandleAsync(CreateModuleModel req, CancellationToken ct)
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
            return; // Ensure method exits if there's an error
        }

        var newModule = Map.ToEntity(req);

        // Save the new module to database.
        await context.CourseElements.AddAsync(newModule);
        await context.SaveChangesAsync(ct);


        // Return the location header only
        var location = $"/courses/{req.CreateModuleBaseModel.ParentId}/modules";
        //Response.Headers.Add("Location", location);

        // Send 201 Created status with no body
        //await SendCreatedAtAsync(location, null, [get], cancellation: ct);

        // Set the Location header with HTTP verb information
        HttpContext.Response.Headers.Add("Location", location);
        HttpContext.Response.Headers.Add("Allow", "GET"); // Add Allow header for HTTP verb

        // Send 201 Created status with no body
        await SendAsync(new()
        {},cancellation: ct); // Specify 'object' explicitly
    }
}
