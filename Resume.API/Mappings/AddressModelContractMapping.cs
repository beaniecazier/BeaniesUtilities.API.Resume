using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.Contracts.Requests;
using Gay.TCazier.Resume.Contracts.Responses;

namespace Gay.TCazier.Resume.API.Mappings;

public static class AddressModelContractMapping
{
    public static AddressModel MapToModelFromCreateRequest(this CreateAddressModelRequest request, int id, string username)
    {
        return new AddressModel(id, request.Name, username, request.Notes)
        {
            HouseNumber = request.HouseNumber,
            StreetName = request.StreetName,
            StreetType = request.StreetType,
            City = request.City,
            Region = request.Region,
            State = request.State,
            Country = request.Country,
            PostalCode = request.PostalCode,
            Zip4 = request.Zip4,
            CrossStreetName = request.CrossStreetName,
            PrefixDirection = request.PrefixDirection,
            PrefixType = request.PrefixType,
            SuffixDirection = request.SuffixDirection,
            SuffixType = request.SuffixType,

        };
    }

    public static AddressModel MapToModelFromUpdateRequest(this UpdateAddressModelRequest request, AddressModel model, string username)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new AddressModel(request.Id, name, username, request.Notes)
        {
            HouseNumber = request.HouseNumber is null ? model.HouseNumber : request.HouseNumber.Value,
            StreetName = request.StreetName is null ? model.StreetName : request.StreetName,
            StreetType = request.StreetType is null ? model.StreetType : request.StreetType,
            City = request.City is null ? model.City : request.City,
            Region = request.Region is null ? model.Region : request.Region,
            State = request.State is null ? model.State : request.State,
            Country = request.Country is null ? model.Country : request.Country,
            PostalCode = request.PostalCode is null ? model.PostalCode : request.PostalCode.Value,
            Zip4 = request.Zip4 is null ? model.Zip4 : request.Zip4,
            CrossStreetName = request.CrossStreetName is null ? model.CrossStreetName : request.CrossStreetName,
            PrefixDirection = request.PrefixDirection is null ? model.PrefixDirection : request.PrefixDirection,
            PrefixType = request.PrefixType is null ? model.PrefixType : request.PrefixType,
            SuffixDirection = request.SuffixDirection is null ? model.SuffixDirection : request.SuffixDirection,
            SuffixType = request.SuffixType is null ? model.SuffixType : request.SuffixType,
        };
    }

    public static AddressModelResponse MapToResponseFromModel(this AddressModel model)
    {
        return new AddressModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,

            HouseNumber = model.HouseNumber,
            StreetName = model.StreetName,
            StreetType = model.StreetType,
            City = model.City,
            Region = model.Region,
            State = model.State,
            Country = model.Country,
            PostalCode = model.PostalCode,
            Zip4 = model.Zip4,
            CrossStreetName = model.CrossStreetName,
            PrefixDirection = model.PrefixDirection,
            PrefixType = model.PrefixType,
            SuffixDirection = model.SuffixDirection,
            SuffixType = model.SuffixType,
        };
    }
}
