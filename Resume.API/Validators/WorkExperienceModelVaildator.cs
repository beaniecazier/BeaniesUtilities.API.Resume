using FluentValidation;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Validators;

public class WorkExperienceModelVaildator : AbstractValidator<EditibleWorkExperienceModel>
{
    public WorkExperienceModelVaildator()
    {
        //RuleFor(entry => entry.StreetName)
        //    .Matches(@".*")
        //    .WithMessage("Value was not a valid street name");
    }
}