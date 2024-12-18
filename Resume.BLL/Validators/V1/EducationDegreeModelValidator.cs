using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class EducationDegreeModelValidator : AbstractValidator<EducationDegreeModel>
{
    public EducationDegreeModelValidator()
    {
		RuleFor(x => x.GPA).NotEmpty().WithMessage("The GPA Property of EducationDegreeModel cannot be empty");
		RuleFor(x => x.Institution).NotEmpty().WithMessage("The Institution Property of EducationDegreeModel cannot be empty");
    }
}

#pragma warning restore CS1591