using Users.Students.GetAllStudents;

namespace Courses.CreateCourse;

//public class Endpoint : EndpointWithoutRequest<IEnumerable<CourseModel>, Mapper>
public class Endpoint : Endpoint<CoursePostModel, IEnumerable<CourseModel>, Mapper>
{

    public override void Configure()
    {
        Post("/courses");
    }

    public override async Task HandleAsync(CoursePostModel r, CancellationToken ct)
    {
        IEnumerable<Course> courses;
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();
        courses = await context.CourseElements.OfType<Course>()
                                               .ToListAsync(ct);

        var courseModels = courses.Select(Map.FromEntity).ToList();

        await SendAsync(courseModels, cancellation: ct);
    }
}



//namespace Author.Signup;

//public class Endpoint : Endpoint<Request, Response, Mapper>
//{
//    public override void Configure()
//    {
//        Post("/author/signup");
//        AllowAnonymous();
//    }

//    public override async Task HandleAsync(Request r, CancellationToken c)
//    {
//        await SendAsync(new Response()
//        {
//            //blank for now
//        });
//    }
}