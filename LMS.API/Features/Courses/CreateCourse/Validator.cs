using FluentValidation;

namespace Courses.CreateCourse;

public class Validator : Validator<CoursePostModel>
{
    public Validator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("A course name is required!")
            //.Custom(c => { DateTime.TryParse(c.Text, out temp)})
            .MinimumLength(3).WithMessage("name is too short!")
            .MaximumLength(25).WithMessage("name is too long!");

        //RuleFor(x => x.StartDate)
        //    .NotEmpty().WithMessage("email address is required!");

        //RuleFor(x => x.EndDate)
        //    .NotEmpty().WithMessage("a username is required!")
        //    .MinimumLength(3).WithMessage("username is too short!")
        //    .MaximumLength(15).WithMessage("username is too long!");
    }

}

