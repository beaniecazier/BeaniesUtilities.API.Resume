using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class ProjectModelContractMapping
{
    public static ProjectModel MapToModelFromCreateRequest(this CreateProjectModelRequest request,
        int id, string username, List<TechTagModel> techTagses)
    {
        return new ProjectModel(id, request.Name, username, request.Notes)
        {
			Description = request.Description,
			Version = request.Version,
			ProjectUrl = request.ProjectUrl,
			StartDate = request.StartDate,
			CompletionDate = request.CompletionDate,
			TechTags = techTagses,
        };
    }

    public static ProjectModel MapToModelFromUpdateRequest(this UpdateProjectModelRequest request, ProjectModel model,
        string username, List<TechTagModel> techTagses)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new ProjectModel(request.Id, name, username, request.Notes)
        {
			Description = string.IsNullOrWhiteSpace(request.Description) ? model.Description : request.Description,
			Version = string.IsNullOrWhiteSpace(request.Version) ? model.Version : request.Version,
			ProjectUrl = string.IsNullOrWhiteSpace(request.ProjectUrl) ? model.ProjectUrl : request.ProjectUrl,
			StartDate = request.StartDate is null ? model.StartDate : request.StartDate.Value,
			CompletionDate = request.CompletionDate is null ? model.CompletionDate : request.CompletionDate.Value,
			TechTags = request.TechTags.Count() <= 0 ? model.TechTags : techTagses,
        };
    }

    public static GetAllProjectModelsOptions MapToOptions(this GetAllProjectModelsRequest options)
    {
        return new GetAllProjectModelsOptions()
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
        };
    }

    public static GetAllProjectModelsOptions WithID(this GetAllProjectModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static ProjectModelResponse MapToResponseFromModel(this ProjectModel model)
    {
        return new ProjectModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			Description = model.Description,
			Version = model.Version,
			ProjectUrl = model.ProjectUrl,
			StartDate = model.StartDate,
			CompletionDate = model.CompletionDate,
			TechTags = model.TechTags,
        };
    }
}

#pragma warning restore CS1591