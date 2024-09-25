namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditiblePhoneNumberModel : EditibleBaseModel
{
    public int CountryCode { get; set; }
    public int AreaCode { get; set; }
    public int TelephonePrefix { get; set; }
    public int LineNumber { get; set; }
}