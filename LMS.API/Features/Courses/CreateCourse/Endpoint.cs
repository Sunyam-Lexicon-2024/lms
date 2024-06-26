using Users.Students.GetAllStudents;

namespace Courses.CreateCourse;

//public class Endpoint : EndpointWithoutRequest<IEnumerable<CourseModel>, Mapper>
public class Endpoint : Endpoint<CoursePostModel, IEnumerable<CourseModel>, Mapper>
{

    public override void Configure()
    {
        Post("/courses");
    }

    public override async Task HandleAsync(CoursePostModel request, CancellationToken ct)
    {
        IEnumerable<Course> courses;
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
        }

        using var context = contextFactory.CreateDbContext();

        var newCourse = Map.ToEntity(request);

        context.CourseElements.Add(newCourse);
        await context.SaveChangesAsync();

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

//public override async Task HandleAsync(Request r, CancellationToken c)
//{
//    var author = Map.ToEntity(r);

//    var emailIsTaken = await Data.EmailAddressIsTaken(author.Email);

//    if (emailIsTaken)
//        AddError(r => r.Email, "Sorry! Email address is already in use...");

//    var userNameIsTaken = await Data.UserNameIsTaken(author.UserName);

//    if (userNameIsTaken)
//        AddError(r => r.UserName, "Sorry! Ehat username is not available...");

//    ThrowIfAnyErrors();

//    await Data.CreateNewAuthor(author);

//    await SendAsync(new()
//    {
//        Message = "Thank you for signing up as an author!"
//    });
//}
//}