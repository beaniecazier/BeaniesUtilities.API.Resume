using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class AddressModelTestMappingExtensions
{
    public static UpdateAddressModelRequest MapToUpdateRequest(this AddressModelResponse createdModel)
    {
        return new UpdateAddressModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            HouseNumber = createdModel.HouseNumber,
            StreetName = createdModel.StreetName,
            StreetType = createdModel.StreetType,
            City = createdModel.City,
            Region = createdModel.Region,
            State = createdModel.State,
            Country = createdModel.Country,
            PostalCode = createdModel.PostalCode,
            Zip4 = createdModel.Zip4,
            CrossStreetName = createdModel.CrossStreetName,
            PrefixDirection = createdModel.PrefixDirection,
            PrefixType = createdModel.PrefixType,
            SuffixDirection = createdModel.SuffixDirection,
            SuffixType = createdModel.SuffixType,
        };
    }
}
