using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface ITechTagModelService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(TechTagModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(TechTagModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllTechTagModelsOptions options);

    Task<Fin<IEnumerable<TechTagModel>>> GetAllAsync(GetAllTechTagModelsOptions options, CancellationToken token = default);
    Task<Fin<TechTagModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(TechTagModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(TechTagModel request, TechTagModel oldModel, CancellationToken token = default);

    Task<Fin<TechTagModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591