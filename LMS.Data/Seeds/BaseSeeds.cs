using LMS.Data.DbContexts;
using LMS.Core.Entities;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace LMS.Data.Seeds;

public class BaseSeeds(LmsDbContext context)
{

    private readonly List<User> _users = [];
    private readonly List<Course> _courses = [];
    private readonly List<Document> _documents = [];

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
            await GenerateDocuments(10);
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
        var users = _context.Users.OfType<Teacher>().ToList();
        var courseElements = _context.CourseElements.OfType<Course>().ToList();

        if (!users.Any() || !courseElements.Any())
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