using Lms.Data.DbContexts;
using LMS.Core.Entities;
using Bogus;

namespace LMS.Data.Seeds;

public class BaseSeeds(LmsDbContext context)
{

    private readonly List<User> _users = [];
    private readonly List<Course> _courses = [];
    private readonly List<Module> _modules = [];
    private readonly List<Activity> _activities = [];

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
                Course courseToAdd = new()
                {
                    Name = $"Course-{i + 1}",
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
        var course = _faker.PickRandom(courses);

        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
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
        var course = _faker.PickRandom(courses);
        var module = _faker.PickRandom(course.ChildElements);

        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                Activity activityToAdd = new()
                {
                    Name = $"{module.Name}-Activity-{i + 1}",
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
                Teacher teacherToAdd = new()
                {
                    Name = $"{first} {last}",
                    Email = $"{first.ToLower()}.{last.ToLower()}@lms-school.com",
                    Password = _faker.Internet.Password(),
                };
                teacherToAdd.Courses.Add(_faker.PickRandom(courses));
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
                Course course = _faker.PickRandom(courses);
                Student studentToAdd = new()
                {
                    Name = $"{first} {last}",
                    Email = $"{first}.{last}@{domain}",
                    Password = _faker.Internet.Password(),
                    CourseId = course.Id,
                    Course = course
                };
                _users.Add(studentToAdd);
            }
        });
    }
}