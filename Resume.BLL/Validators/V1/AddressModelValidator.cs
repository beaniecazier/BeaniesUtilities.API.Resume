using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class AddressModelValidator : AbstractValidator<AddressModel>
{
    public AddressModelValidator()
    {
		RuleFor(x => x.HouseNumber).NotEmpty().WithMessage("The HouseNumber Property of AddressModel cannot be empty");
		RuleFor(x => x.StreetName).NotEmpty().WithMessage("The StreetName Property of AddressModel cannot be empty");
		RuleFor(x => x.StreetType).NotEmpty().WithMessage("The StreetType Property of AddressModel cannot be empty");
		RuleFor(x => x.City).NotEmpty().WithMessage("The City Property of AddressModel cannot be empty");
		RuleFor(x => x.State).NotEmpty().WithMessage("The State Property of AddressModel cannot be empty");
		RuleFor(x => x.Country).NotEmpty().WithMessage("The Country Property of AddressModel cannot be empty");
		RuleFor(x => x.PostalCode).NotEmpty().WithMessage("The PostalCode Property of AddressModel cannot be empty");
    }
}

#pragma warning restore CS1591