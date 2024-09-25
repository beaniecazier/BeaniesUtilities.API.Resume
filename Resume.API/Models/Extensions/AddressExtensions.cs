using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using System.ComponentModel.DataAnnotations;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class AddressExtensions
{
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
