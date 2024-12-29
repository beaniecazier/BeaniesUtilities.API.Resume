using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

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

    public static PersonModel MapToModelFromUpdateRequest(this UpdatePersonModelRequest request, PersonModel model,
        string username, List<AddressModel> addresseses, List<PhoneNumberModel> phoneNumberses)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new PersonModel(request.Id, name, username, request.Notes)
        {
			PreferedName = string.IsNullOrWhiteSpace(request.PreferedName) ? model.PreferedName : request.PreferedName,
			Pronouns = request.Pronouns.Count() <= 0 ? model.Pronouns : request.Pronouns,
			Emails = request.Emails.Count() <= 0 ? model.Emails : request.Emails,
			Socials = request.Socials.Count() <= 0 ? model.Socials : request.Socials,
			Addresses = request.Addresses.Count() <= 0 ? model.Addresses : addresseses,
			PhoneNumbers = request.PhoneNumbers.Count() <= 0 ? model.PhoneNumbers : phoneNumberses,
        };
    }

    public static GetAllPersonModelsOptions MapToOptions(this GetAllPersonModelsRequest options)
    {
        return new GetAllPersonModelsOptions()
        {
            NameSearchTerm = options.NameSearchTerm,
            NotesSearchTerm = options.NotesSearchTerm,
            AfterDate = options.AfterDate,
            BeforeDate = options.BeforeDate,
            AllowHidden = options.AllowHidden,
            AllowDeleted = options.AllowDeleted,
            GreaterThanOrEqualToID = options.GreaterThanOrEqualToID,
            LessThanOrEqualToID = options.LessThanOrEqualToID,
            SpecificIds = options.SpecificIds,
            PageIndex = options.PageIndex,
            PageSize = options.PageSize,
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