using Users.Students.GetAllStudents;

namespace Courses.CreateCourse;

public class Endpoint : Endpoint<CoursePostModel, Response, Mapper>
{
    

    public override void Configure()
    {
        Post("/courses");
    }

    public override async Task HandleAsync(CoursePostModel request, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        var newCourse = Map.ToEntity(request);

        context.CourseElements.Add(newCourse);
        await context.SaveChangesAsync();


        await SendAsync(new Response()
        {
            Message = $"The course '{request.Name}' was added."
        });
    }
}