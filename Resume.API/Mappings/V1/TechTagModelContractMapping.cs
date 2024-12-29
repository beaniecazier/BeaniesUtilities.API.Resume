using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class TechTagModelContractMapping
{
    public static TechTagModel MapToModelFromCreateRequest(this CreateTechTagModelRequest request,
        int id, string username)
    {
        return new TechTagModel(id, request.Name, username, request.Notes)
        {
			URL = request.URL,
			Description = request.Description,
        };
    }

    public static TechTagModel MapToModelFromUpdateRequest(this UpdateTechTagModelRequest request, TechTagModel model,
        string username)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new TechTagModel(request.Id, name, username, request.Notes)
        {
			URL = string.IsNullOrWhiteSpace(request.URL) ? model.URL : request.URL,
			Description = string.IsNullOrWhiteSpace(request.Description) ? model.Description : request.Description,
        };
    }

    public static GetAllTechTagModelsOptions MapToOptions(this GetAllTechTagModelsRequest options)
    {
        return new GetAllTechTagModelsOptions()
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

    public static GetAllTechTagModelsOptions WithID(this GetAllTechTagModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static TechTagModelResponse MapToResponseFromModel(this TechTagModel model)
    {
        return new TechTagModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			URL = model.URL,
			Description = model.Description,
        };
    }
}

#pragma warning restore CS1591