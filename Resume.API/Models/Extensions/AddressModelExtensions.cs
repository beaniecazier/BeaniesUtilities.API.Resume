using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class AddressExtensions
{
    public static void Create(this AddressModel model, int id, EditibleAddressModel props, string user)
    {
		model.HouseNumber = props.HouseNumber;
		model.StreetName = props.StreetName;
		model.StreetType = props.StreetType;
		model.City = props.City;
		model.Region = props.Region;
		model.State = props.State;
		model.Country = props.Country;
		model.PostalCode = props.PostalCode;
		model.Zip4 = props.Zip4;
		model.CrossStreetName = props.CrossStreetName;
		model.PrefixDirection = props.PrefixDirection;
		model.PrefixType = props.PrefixType;
		model.SuffixDirection = props.SuffixDirection;
		model.SuffixType = props.SuffixType;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static AddressModel Update(this AddressModel model, EditibleAddressModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new AddressModel
        {
			HouseNumber = updates.HouseNumber,
			StreetName = updates.StreetName,
			StreetType = updates.StreetType,
			City = updates.City,
			Region = updates.Region,
			State = updates.State,
			Country = updates.Country,
			PostalCode = updates.PostalCode,
			Zip4 = updates.Zip4,
			CrossStreetName = updates.CrossStreetName,
			PrefixDirection = updates.PrefixDirection,
			PrefixType = updates.PrefixType,
			SuffixDirection = updates.SuffixDirection,
			SuffixType = updates.SuffixType,

            Name = updates.Name,
            IsHidden = updates.IsHidden,

            CommonIdentity = model.CommonIdentity,
            CreatedBy = model.CreatedBy,
            CreatedOn = model.CreatedOn,
            IsDeleted = model.IsDeleted,

            ModifiedBy = user,
            ModifiedOn = DateTime.UtcNow,
            Notes = "Edited",
        };
    }
}