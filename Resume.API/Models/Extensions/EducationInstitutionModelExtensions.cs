using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class EducationInstitutionExtensions
{
    public static void Create(this EducationInstitutionModel model, int id, EditibleEducationInstitutionModel props, string user)
    {
		model.Website = props.Website;
		model.Address = props.Address;
		model.CertificatesIssued = props.CertificatesIssued;
		model.DegreesGiven = props.DegreesGiven;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static EducationInstitutionModel Update(this EducationInstitutionModel model, EditibleEducationInstitutionModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new EducationInstitutionModel
        {
			Website = updates.Website,
			Address = updates.Address,
			CertificatesIssued = updates.CertificatesIssued,
			DegreesGiven = updates.DegreesGiven,

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