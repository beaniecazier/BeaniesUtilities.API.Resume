using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleEducationDegreeModel : EditibleBaseModel
{
	public float GPA { get; set; } 
	public DateTime? StartDate { get; set; } 
	public DateTime? EndDate { get; set; } 
	public EducationInstitutionModel Institution { get; set; } 

}