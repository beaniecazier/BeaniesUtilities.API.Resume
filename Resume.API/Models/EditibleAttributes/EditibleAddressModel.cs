using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleAddressModel : EditibleBaseModel
{
	public int HouseNumber { get; set; } 
	public string StreetName { get; set; } 
	public string StreetType { get; set; } 
	public string City { get; set; } 
	public string Region { get; set; } 
	public string State { get; set; } 
	public string Country { get; set; } 
	public int PostalCode { get; set; } 
	public int? Zip4 { get; set; } 
	public string CrossStreetName { get; set; } 
	public string PrefixDirection { get; set; } 
	public string PrefixType { get; set; } 
	public string SuffixDirection { get; set; } 
	public string SuffixType { get; set; } 

}