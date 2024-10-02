using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleCertificateModel : EditibleBaseModel
{
	public DateTime IssueDate { get; set; } 
	public string Link { get; set; } 
	public string PdfFileName { get; set; } 
	public EducationInstitutionModel Issuer { get; set; } 

}