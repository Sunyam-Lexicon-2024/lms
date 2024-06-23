using Lms.Data.DbContexts;
using LMS.Core.Entities;
using Bogus;

namespace LMS.Data.Seeds;

public class BaseSeeds(LmsDbContext context)
{

    private readonly List<User> _users = [];
    private readonly List<Course> _courses = [];

    private readonly LmsDbContext _context = context;
    private readonly Faker _faker = new();

    public async Task InitAsync()
    {
        try
        {
            await GenerateCourses(3);
            _context.CourseElements.AddRange(_courses);
            await _context.SaveChangesAsync();

            await GenerateStudents(50);
            await GenerateTeachers(5);
            _context.Users.AddRange(_users);
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
                    PasswordHash = _faker.Internet.Password(),
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
                    PasswordHash = _faker.Internet.Password(),
                    CourseId = course.Id,
                    Course = course
                };
                _users.Add(studentToAdd);
            }
        });
    }
}