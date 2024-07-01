using LMS.Data.DbContexts;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace LMS.Data.Seeds;

public class BaseSeeds(LmsDbContext context)
{

    private readonly List<User> _users = [];
    private readonly List<Course> _courses = [];
    private readonly List<Document> _documents = [];
    private readonly List<Module> _modules = [];
    private readonly List<ModuleActivity> _activities = [];

    private readonly LmsDbContext _context = context;
    private readonly Faker _faker = new();

    public async Task InitAsync()
    {
        try
        {
            await GenerateCourses(3);
            await _context.CourseElements.AddRangeAsync(_courses);
            await _context.SaveChangesAsync();

            await GenerateModules(30);
            await _context.CourseElements.AddRangeAsync(_modules);
            await _context.SaveChangesAsync();

            await GenerateActivities(90);
            await _context.CourseElements.AddRangeAsync(_activities);
            await _context.SaveChangesAsync();

            await GenerateStudents(50);
            await GenerateTeachers(5);
            await _context.Users.AddRangeAsync(_users);
            await _context.SaveChangesAsync();
            await GenerateDocuments(100);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task GenerateCourses(int count)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                var coursenumber = i + 1;
                Course courseToAdd = new()
                {
                    Name = $"Course-{coursenumber}",
                    Description = $"Course-{coursenumber} description",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
                };

                _courses.Add(courseToAdd);
            }
        });
    }

    private async Task GenerateModules(int count)
    {
        var courses = _context.CourseElements.OfType<Course>().ToList();

        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                var course = _faker.PickRandom(courses);
                Module moduleToAdd = new()
                {
                    Name = $"{course.Name}-Module-{i + 1}",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                    Parent = course,
                    ParentId = course.Id
                };

                _modules.Add(moduleToAdd);
            }
        });
    }

    private async Task GenerateActivities(int count)
    {
        var courses = _context.CourseElements.OfType<Course>()
                                            .Where(c => c.ChildElements.Count > 0)
                                            .ToList();

        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                var course = _faker.PickRandom(courses);
                var module = _faker.PickRandom(course.ChildElements);
                ModuleActivity activityToAdd = new()
                {
                    Name = $"{module.Name}-Activity-{i + 1}",
                    Description = $"{module.Name} Description",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                    Parent = module,
                    ParentId = module.Id,
                    Type = _faker.PickRandom<ActivityType>()
                };

                _activities.Add(activityToAdd);
            }
        });
    }

    private async Task GenerateTeachers(int count)
    {
        var courses = _context.CourseElements.OfType<Course>().ToList();
        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                string first = _faker.Name.FirstName();
                string last = _faker.Name.LastName();
                string domain = _faker.Internet.DomainName();
                string email = $"{first}.{last}@{domain}";
                var password = new PasswordHasher<Teacher>();
                Teacher teacherToAdd = new()
                {
                    Name = $"{first} {last}",
                    Email = email.ToLower(),
                    NormalizedEmail = email.ToUpper(),
                    UserName = email.ToLower(),
                    NormalizedUserName = email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = _faker.Phone.PhoneNumber(),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };
                teacherToAdd.Courses.Add(_faker.PickRandom(courses));
                var hashed = password.HashPassword(teacherToAdd, "secret");
                teacherToAdd.PasswordHash = hashed;
                _users.Add(teacherToAdd);
            }
        });
    }

    private async Task GenerateStudents(int count)
    {
        var courses = _context.CourseElements.OfType<Course>().ToList();
        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                string first = _faker.Name.FirstName();
                string last = _faker.Name.LastName();
                string domain = _faker.Internet.DomainName();
                string email = $"{first}.{last}@{domain}";
                var password = new PasswordHasher<Student>();
                Course course = _faker.PickRandom(courses);
                Student studentToAdd = new()
                {
                    Name = $"{first} {last}",
                    Email = email.ToLower(),
                    NormalizedEmail = email.ToUpper(),
                    UserName = email.ToLower(),
                    NormalizedUserName = email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = _faker.Phone.PhoneNumber(),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    CourseId = course.Id,
                    Course = course
                };
                var hashed = password.HashPassword(studentToAdd, "secret");
                studentToAdd.PasswordHash = hashed;
                _users.Add(studentToAdd);
            }
        });
    }

    // Generate Documents
     private async Task GenerateDocuments(int count)
    {
        var users = _context.Users.OfType<Student>().ToList();
        var courseElements = _context.CourseElements.OfType<Course>().ToList();

        if (users.Count == 0 || courseElements.Count == 0)
        {
            throw new InvalidOperationException("Users or CourseElements are not available for generating documents.");
        }

        await Task.Run(async () =>
        {
            for (int i = 0; i < count; i++)
            {
                string name = _faker.Lorem.Sentence();
                string url = _faker.Internet.Url();
                string description = _faker.Lorem.Paragraph();
                DateTime uploadedAt = _faker.Date.Past(1);
                User uploader = _faker.PickRandom(users);
                CourseElement courseElement = _faker.PickRandom(courseElements);

                Document documentToAdd = new()
                {
                    Name = name,
                    Url = url,
                    Description = description,
                    UploadedAt = uploadedAt,
                    CourseElementId = courseElement.Id,
                    CourseElement = courseElement,
                    UploaderId = uploader.Id,
                    User = uploader
                };

                _documents.Add(documentToAdd);
            }
            _context.Documents.AddRange(_documents);
            await _context.SaveChangesAsync();
        });
    }


}