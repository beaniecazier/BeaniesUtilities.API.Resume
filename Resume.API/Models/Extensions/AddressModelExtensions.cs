using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

/// <summary>
/// A collection of extension methods for the Address Model
/// </summary>
public static class AddressExtensions
{
	/// <summary>
	/// "Create" a new Address Model, by applying a set of properties based on the permissions of the user
	/// </summary>
	/// <param name="model">The model that is getting created</param>
	/// <param name="id">The model's id that it will use</param>
	/// <param name="props">The properties to be used in the model</param>
	/// <param name="user">The user adding the model</param>
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

    /// <summary>
    /// Update Address Model of id by applying a set of properties based on the permissions of the user
    /// </summary>
    /// <param name="model">Model to update</param>
    /// <param name="updates">New parameters of model</param>
    /// <param name="user">User doing the update</param>
    /// <returns></returns>
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