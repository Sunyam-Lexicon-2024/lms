using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using Users.Students.GetAllStudents;

namespace Courses.CreateCourse;

public class Endpoint : Endpoint<CoursePostModel,
    Results<Ok<Response>, BadRequest<string>>,
    Mapper>
{
    

    public override void Configure()
    {
        Post("/courses");
    }

    public override async Task<Results<Ok<Response>,BadRequest<string>>> ExecuteAsync(CoursePostModel request, CancellationToken ct)
    {
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        var newCourse = Map.ToEntity(request);

        var addedCourse = await context.CourseElements.AddAsync(newCourse);
        await context.SaveChangesAsync();

        var course = addedCourse.Entity as Course;

        if (course is not null)
        {
            return TypedResults.Ok(Map.FromEntity(course));
        }

        return TypedResults.BadRequest("Could not create the Course.");
    }
}