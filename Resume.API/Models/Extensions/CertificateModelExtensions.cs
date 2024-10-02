using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class CertificateExtensions
{
    public static void Create(this CertificateModel model, int id, EditibleCertificateModel props, string user)
    {
		model.IssueDate = props.IssueDate;
		model.Link = props.Link;
		model.PdfFileName = props.PdfFileName;
		model.Issuer = props.Issuer;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static CertificateModel Update(this CertificateModel model, EditibleCertificateModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new CertificateModel
        {
			IssueDate = updates.IssueDate,
			Link = updates.Link,
			PdfFileName = updates.PdfFileName,
			Issuer = updates.Issuer,

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