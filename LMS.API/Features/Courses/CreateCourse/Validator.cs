using FluentValidation;

namespace Courses.CreateCourse;

public class Validator : Validator<CoursePostModel>
{
    public Validator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("A course name is required!")
            .MaximumLength(255).WithMessage("The course name is too long");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("A description is required!");

        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("Start date is required to follow the format \"YYYY-MM-DD\"!")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Activity end date must be current or future date");

        RuleFor(c => c.EndDate)
            .NotEmpty().WithMessage("End date is required to follow the format \"YYYY-MM-DD\"!")
            .GreaterThanOrEqualTo(c => c.StartDate).WithMessage("End date must not occur before the start date!");
    }
}

