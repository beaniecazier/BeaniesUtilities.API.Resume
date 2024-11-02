using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators;

#pragma warning disable CS1591

public class AddressModelValidator : AbstractValidator<AddressModel>
{
    public AddressModelValidator()
    {
        //RuleFor(entry => entry.StreetName)
        //    .Matches(@".*")
        //    .WithMessage("Value was not a valid street name");
    }
}

#pragma warning restore CS1591