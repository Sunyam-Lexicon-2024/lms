namespace LMS.API.Features.Courses.Modules.CreateModule
{
    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Module name required")
                .MinimumLength(5)
                .WithMessage("Module name is too short")
                .MaximumLength(50)
                .WithMessage("Module name is too long");

            RuleFor(r => r.Description)
               .NotEmpty()
               .WithMessage("Module description required")
               .MinimumLength(10)
               .WithMessage("Module description is too short")
               .MaximumLength(200)
               .WithMessage("Module description is too long");

            RuleFor(r => r.ParentId)
                .NotNull()
                .WithMessage("Module must be assigned to a course");

            RuleFor(r => r.EndDate)
                .GreaterThanOrEqualTo(r => r.StartDate)
                .WithMessage("Module end date cannot be before start date");
        }
    }
}
