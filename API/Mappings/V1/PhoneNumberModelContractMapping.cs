using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class PhoneNumberModelContractMapping
{
    public static PhoneNumberModel MapToModelFromCreateRequest(this CreatePhoneNumberModelRequest request,
        int id, string username)
    {
        return new PhoneNumberModel(id, request.Name, username, request.Notes)
        {
			CountryCode = request.CountryCode,
			AreaCode = request.AreaCode,
			TelephonePrefix = request.TelephonePrefix,
			LineNumber = request.LineNumber,
        };
    }

    public static PhoneNumberModel MapToModelFromUpdateRequest(this UpdatePhoneNumberModelRequest request, PhoneNumberModel model,
        string username)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new PhoneNumberModel(request.Id, name, username, request.Notes)
        {
			CountryCode = request.CountryCode is null ? model.CountryCode : request.CountryCode.Value,
			AreaCode = request.AreaCode is null ? model.AreaCode : request.AreaCode.Value,
			TelephonePrefix = request.TelephonePrefix is null ? model.TelephonePrefix : request.TelephonePrefix.Value,
			LineNumber = request.LineNumber is null ? model.LineNumber : request.LineNumber.Value,
        };
    }

    public static GetAllPhoneNumberModelsOptions MapToOptions(this GetAllPhoneNumberModelsRequest request)
    {
        return new GetAllPhoneNumberModelsOptions()
        {
            NameSearchTerm = request.NameSearchTerm,
            NotesSearchTerm = request.NotesSearchTerm,
            AfterDate = request.AfterDate,
            BeforeDate = request.BeforeDate,
            AllowHidden = request.AllowHidden,
            AllowDeleted = request.AllowDeleted,
            GreaterThanOrEqualToID = request.GreaterThanOrEqualToID,
            LessThanOrEqualToID = request.LessThanOrEqualToID,
            SpecificIds = request.SpecificIds,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            SortField = request.SortBy?.Trim('+', '-'),
            SortOrder = request.SortBy is null ? SortOrder.Unspecified : 
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
        };
    }

    public static GetAllPhoneNumberModelsOptions WithID(this GetAllPhoneNumberModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static PhoneNumberModelResponse MapToResponseFromModel(this PhoneNumberModel model)
    {
        return new PhoneNumberModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			CountryCode = model.CountryCode,
			AreaCode = model.AreaCode,
			TelephonePrefix = model.TelephonePrefix,
			LineNumber = model.LineNumber,
        };
    }
}

#pragma warning restore CS1591