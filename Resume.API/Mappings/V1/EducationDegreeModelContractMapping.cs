using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class EducationDegreeModelContractMapping
{
    public static EducationDegreeModel MapToModelFromCreateRequest(this CreateEducationDegreeModelRequest request,
        int id, string username, EducationInstitutionModel institution)
    {
        return new EducationDegreeModel(id, request.Name, username, request.Notes)
        {
			GPA = request.GPA,
			StartDate = request.StartDate,
			EndDate = request.EndDate,
			Institution = institution,
        };
    }

    public static EducationDegreeModel MapToModelFromUpdateRequest(this UpdateEducationDegreeModelRequest request, EducationDegreeModel model,
        string username, EducationInstitutionModel institution)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new EducationDegreeModel(request.Id, name, username, request.Notes)
        {
			GPA = request.GPA is null ? model.GPA : request.GPA.Value,
			StartDate = request.StartDate is null ? model.StartDate : request.StartDate.Value,
			EndDate = request.EndDate is null ? model.EndDate : request.EndDate.Value,
			Institution = request.Institution is null ? model.Institution : institution,
        };
    }

    public static GetAllEducationDegreeModelsOptions MapToOptions(this GetAllEducationDegreeModelsRequest options)
    {
        return new GetAllEducationDegreeModelsOptions()
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

    public static GetAllEducationDegreeModelsOptions WithID(this GetAllEducationDegreeModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static EducationDegreeModelResponse MapToResponseFromModel(this EducationDegreeModel model)
    {
        return new EducationDegreeModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			GPA = model.GPA,
			StartDate = model.StartDate,
			EndDate = model.EndDate,
			Institution = model.Institution,
        };
    }
}

#pragma warning restore CS1591