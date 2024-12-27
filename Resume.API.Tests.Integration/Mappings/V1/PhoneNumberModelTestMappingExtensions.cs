using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class PhoneNumberModelTestMappingExtensions
{
    public static UpdatePhoneNumberModelRequest MapToUpdateRequest(this PhoneNumberModelResponse createdModel)
    {
        return new UpdatePhoneNumberModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            CountryCode = createdModel.CountryCode,
            AreaCode = createdModel.AreaCode,
            TelephonePrefix = createdModel.TelephonePrefix,
            LineNumber = createdModel.LineNumber,
        };
    }
}
