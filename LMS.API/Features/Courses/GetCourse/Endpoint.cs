using Microsoft.AspNetCore.Http.HttpResults;

namespace Courses.GetCourse;

public class Endpoint : Endpoint<Request, Results<Ok<Response>, NotFound>, Mapper>
{
    public override void Configure()
    {
        Get("/courses/{CourseId}/get-course");
        Description(d =>
         d.Produces<Response>(200, "application/json")
     );
        // Swagger summary
        Summary(s =>
        {
            s.Summary = "Gets course by ID";
            s.Description = "Gets the course by the specified ID value";
            s.ResponseExamples[200] = new CourseResponseModel()
            {
                Name = "My course",
                Description = "My course description",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
            };
        });
    }

    public override async Task<Results<Ok<Response>, NotFound>> ExecuteAsync(Request req, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        var course = await context.CourseElements.OfType<Course>()
                                                .FirstOrDefaultAsync(c => c.Id == req.CourseId, ct);
        if (course is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new Response() { StudentCourse = Map.FromEntity(course) });
    }
}
