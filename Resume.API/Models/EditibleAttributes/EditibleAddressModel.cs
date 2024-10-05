using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

#pragma warning disable CS1591

public class EditibleAddressModel : EditibleBaseModel
{
    public int HouseNumber { get; set; }
    public string StreetName { get; set; }
    public string StreetType { get; set; }
    public string City { get; set; }
    public string? Region { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public int PostalCode { get; set; }
    public int? Zip4 { get; set; }
    public string? CrossStreetName { get; set; }
    public string? PrefixDirection { get; set; }
    public string? PrefixType { get; set; }
    public string? SuffixDirection { get; set; }
    public string? SuffixType { get; set; }

    public EditibleAddressModel(AddressModel model, int id) : base(id)
    {
        Name = model.Name;

        //Model specific parameters here
        HouseNumber = model.HouseNumber;
        StreetName = model.StreetName;
        StreetType = model.StreetType;
        City = model.City;
        Region = model.Region;
        State = model.State;
        Country = model.Country;
        PostalCode = model.PostalCode;
        Zip4 = model.Zip4;
        CrossStreetName = model.CrossStreetName;
        PrefixDirection = model.PrefixDirection;
        PrefixType = model.PrefixType;
        SuffixDirection = model.SuffixDirection;
        SuffixType = model.SuffixType;

        IsHidden = model.IsHidden;
    }
}

#pragma warning restore CS1591