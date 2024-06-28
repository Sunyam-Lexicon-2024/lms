namespace Courses.Modules.Activities.CreateActivity;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Activity name required")
            .MinimumLength(5)
            .WithMessage("Activity name is too short")
            .MaximumLength(50)
            .WithMessage("Activity name is too long");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Activity description required")
            .MinimumLength(10)
            .WithMessage("Activity description is too short")
            .MaximumLength(200)
            .WithMessage("Activity description is too long");

        RuleFor(r => r.ParentId)
            .NotNull()
            .WithMessage("Activity must be assigned to module");

        RuleFor(r => r.EndDate)
            .GreaterThanOrEqualTo(r => r.StartDate)
            .WithMessage("Activity end date cannot be before start date");

        RuleFor(r => r.StartDate)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Activity start date must be current or future date");
        
        RuleFor(r => r.EndDate)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Activity end date must be current or future date");
    }
}