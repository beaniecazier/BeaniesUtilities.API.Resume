using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

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

    public static AddressModel MapToModelFromUpdateRequest(this UpdateAddressModelRequest request, string username)
    {
        string name = request.Name;
        return new AddressModel(request.Id, name, username, request.Notes)
        {
			HouseNumber = request.HouseNumber.Value,
			StreetName = request.StreetName,
			StreetType = request.StreetType,
			City = request.City,
			Region = request.Region,
			State = request.State,
			Country = request.Country,
			PostalCode = request.PostalCode.Value,
			Zip4 = request.Zip4.Value,
			CrossStreetName = request.CrossStreetName,
			PrefixDirection = request.PrefixDirection,
			PrefixType = request.PrefixType,
			SuffixDirection = request.SuffixDirection,
			SuffixType = request.SuffixType,
        };
    }

    public static GetAllAddressModelsOptions MapToOptions(this GetAllAddressModelsRequest request)
    {
        return new GetAllAddressModelsOptions()
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