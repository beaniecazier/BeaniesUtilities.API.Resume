using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class AddressModelContractMapping
{
    public static AddressModel MapToModelFromCreateRequest(this CreateAddressModelRequest request,
        int id, string username)
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

    public static AddressModel MapToModelFromUpdateRequest(this UpdateAddressModelRequest request, AddressModel model,
        string username)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new AddressModel(request.Id, name, username, request.Notes)
        {
			HouseNumber = request.HouseNumber is null ? model.HouseNumber : request.HouseNumber.Value,
			StreetName = string.IsNullOrWhiteSpace(request.StreetName) ? model.StreetName : request.StreetName,
			StreetType = string.IsNullOrWhiteSpace(request.StreetType) ? model.StreetType : request.StreetType,
			City = string.IsNullOrWhiteSpace(request.City) ? model.City : request.City,
			Region = string.IsNullOrWhiteSpace(request.Region) ? model.Region : request.Region,
			State = string.IsNullOrWhiteSpace(request.State) ? model.State : request.State,
			Country = string.IsNullOrWhiteSpace(request.Country) ? model.Country : request.Country,
			PostalCode = request.PostalCode is null ? model.PostalCode : request.PostalCode.Value,
			Zip4 = request.Zip4 is null ? model.Zip4 : request.Zip4.Value,
			CrossStreetName = string.IsNullOrWhiteSpace(request.CrossStreetName) ? model.CrossStreetName : request.CrossStreetName,
			PrefixDirection = string.IsNullOrWhiteSpace(request.PrefixDirection) ? model.PrefixDirection : request.PrefixDirection,
			PrefixType = string.IsNullOrWhiteSpace(request.PrefixType) ? model.PrefixType : request.PrefixType,
			SuffixDirection = string.IsNullOrWhiteSpace(request.SuffixDirection) ? model.SuffixDirection : request.SuffixDirection,
			SuffixType = string.IsNullOrWhiteSpace(request.SuffixType) ? model.SuffixType : request.SuffixType,
        };
    }

    public static GetAllAddressModelsOptions MapToOptions(this GetAllAddressModelsRequest options)
    {
        return new GetAllAddressModelsOptions()
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

    public static GetAllAddressModelsOptions WithID(this GetAllAddressModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
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

#pragma warning restore CS1591