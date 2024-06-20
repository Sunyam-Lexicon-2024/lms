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
            await GenerateTeachers(5);
            await GenerateStudents(50);


            foreach (var u in _users)
            {
                _context.Users.Add(u);
            }

            foreach (var c in _courses)
            {
                _context.CourseElements.Add(c);
            }
            
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
        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                string first = _faker.Name.FirstName();
                string last = _faker.Name.LastName();
                string domain = _faker.Internet.DomainName();
                User teacherToAdd = new()
                {
                    Name = $"{first} {last}",
                    Email = $"{first}.{last}@$lms-school.com",
                    Password = _faker.Internet.Password(),
                    Role = Role.Teacher
                };
                teacherToAdd.Courses.Add(_faker.PickRandom(_courses));
                _users.Add(teacherToAdd);
            }
        });
    }


    private async Task GenerateStudents(int count)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                string first = _faker.Name.FirstName();
                string last = _faker.Name.LastName();
                string domain = _faker.Internet.DomainName();
                User studentToAdd = new()
                {
                    Name = $"{first} {last}",
                    Email = $"{first}.{last}@${domain}.com",
                    Password = _faker.Internet.Password(),
                    Role = Role.Student
                };
                studentToAdd.Courses.Add(_faker.PickRandom(_courses));
                _users.Add(studentToAdd);
            }
        });
    }
}