using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class PersonModelContractMapping
{
    public static PersonModel MapToModelFromCreateRequest(this CreatePersonModelRequest request,
        int id, string username, List<AddressModel> addresseses, List<PhoneNumberModel> phoneNumberses)
    {
        return new PersonModel(id, request.Name, username, request.Notes)
        {
			PreferedName = request.PreferedName,
			Pronouns = request.Pronouns,
			Emails = request.Emails,
			Socials = request.Socials,
			Addresses = addresseses,
			PhoneNumbers = phoneNumberses,
        };
    }

    public static PersonModel MapToModelFromUpdateRequest(this UpdatePersonModelRequest request, string username, List<AddressModel> addresseses, List<PhoneNumberModel> phoneNumberses)
    {
        string name = request.Name;
        return new PersonModel(request.Id, name, username, request.Notes)
        {
			PreferedName = request.PreferedName,
			Pronouns = request.Pronouns,
			Emails = request.Emails,
			Socials = request.Socials,
			Addresses = addresseses,
			PhoneNumbers = phoneNumberses,
        };
    }

    public static GetAllPersonModelsOptions MapToOptions(this GetAllPersonModelsRequest request)
    {
        return new GetAllPersonModelsOptions()
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

    public static GetAllPersonModelsOptions WithID(this GetAllPersonModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static PersonModelResponse MapToResponseFromModel(this PersonModel model)
    {
        return new PersonModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			PreferedName = model.PreferedName,
			Pronouns = model.Pronouns,
			Emails = model.Emails,
			Socials = model.Socials,
			Addresses = model.Addresses,
			PhoneNumbers = model.PhoneNumbers,
        };
    }
}

#pragma warning restore CS1591