using BeaniesUtilities.Models.Enum;
using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditiblePersonModel : EditibleBaseModel
{
	public string PreferedName { get; set; } 
	public List<ePronoun> Pronouns { get; set; } = new List<ePronoun>();
	public List<string> Emails { get; set; } = new List<string>();
	public List<string> Socials { get; set; } = new List<string>();
	public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
	public List<PhoneNumberModel> PhoneNumbers { get; set; } = new List<PhoneNumberModel>();

}