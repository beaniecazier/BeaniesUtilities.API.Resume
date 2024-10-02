using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class PhoneNumberExtensions
{
    public static void Create(this PhoneNumberModel model, int id, EditiblePhoneNumberModel props, string user)
    {
		model.CountryCode = props.CountryCode;
		model.AreaCode = props.AreaCode;
		model.TelephonePrefix = props.TelephonePrefix;
		model.LineNumber = props.LineNumber;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static PhoneNumberModel Update(this PhoneNumberModel model, EditiblePhoneNumberModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new PhoneNumberModel
        {
			CountryCode = updates.CountryCode,
			AreaCode = updates.AreaCode,
			TelephonePrefix = updates.TelephonePrefix,
			LineNumber = updates.LineNumber,

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