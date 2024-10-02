using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleResumeModel : EditibleBaseModel
{
	public string HeroStatement { get; set; } 
	public List<EducationDegreeModel> Degrees { get; set; } = new List<EducationDegreeModel>();
	public List<CertificateModel> Certificates { get; set; } = new List<CertificateModel>();
	public List<WorkExperienceModel> WorkExperience { get; set; } = new List<WorkExperienceModel>();
	public List<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
	public string PreferedName { get; set; } 
	public List<ePronoun> Pronouns { get; set; } = new List<ePronoun>();
	public List<string> Emails { get; set; } = new List<string>();
	public List<string> Socials { get; set; } = new List<string>();
	public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
	public List<PhoneNumberModel> PhoneNumbers { get; set; } = new List<PhoneNumberModel>();

}