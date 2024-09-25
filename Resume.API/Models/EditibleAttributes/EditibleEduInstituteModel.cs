using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleEduInstituteModel : EditibleBaseModel
{
    public string? Website { get; set; }
    public AddressModel? Address { get; set; }

    public List<CertificateModel> CertificatesIssued { get; set; } = new List<CertificateModel>();
    public List<EducationDegreeModel> DegreesGiven { get; set; } = new List<EducationDegreeModel>();
}