using FluentValidation;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Validators;

#pragma warning disable CS1591

public class AddressModelVaildator : AbstractValidator<EditibleAddressModel>
{
    public AddressModelVaildator()
    {
        //RuleFor(entry => entry.StreetName)
        //    .Matches(@".*")
        //    .WithMessage("Value was not a valid street name");
    }
}

#pragma warning restore CS1591