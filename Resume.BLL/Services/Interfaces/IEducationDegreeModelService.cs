using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IEducationDegreeModelService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(EducationDegreeModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(EducationDegreeModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllEducationDegreeModelsOptions options);

    Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllAsync(GetAllEducationDegreeModelsOptions options, CancellationToken token = default);
    Task<Fin<EducationDegreeModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(EducationDegreeModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(EducationDegreeModel request, EducationDegreeModel oldModel, CancellationToken token = default);

    Task<Fin<EducationDegreeModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591