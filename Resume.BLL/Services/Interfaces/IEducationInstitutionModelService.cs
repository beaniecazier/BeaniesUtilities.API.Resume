using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IEducationInstitutionModelService
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllEducationInstitutionModelsOptions options);

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(EducationInstitutionModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(EducationInstitutionModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllEducationInstitutionModelsOptions options);

    Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllAsync(GetAllEducationInstitutionModelsOptions options, CancellationToken token = default);
    Task<Fin<EducationInstitutionModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(EducationInstitutionModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(EducationInstitutionModel request, EducationInstitutionModel oldModel, CancellationToken token = default);

    Task<Fin<EducationInstitutionModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591