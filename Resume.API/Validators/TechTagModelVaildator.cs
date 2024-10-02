using FluentValidation;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Validators;

public class TechTagModelVaildator : AbstractValidator<EditibleTechTagModel>
{
    public TechTagModelVaildator()
    {
        //RuleFor(entry => entry.StreetName)
        //    .Matches(@".*")
        //    .WithMessage("Value was not a valid street name");
    }
}