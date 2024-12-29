using BeaniesUtilities.Models.Resume;
using LanguageExt;
using LanguageExt.Common;
using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Gay.TCazier.Resume.BLL.Options.V1;

namespace Gay.TCazier.Resume.BLL.Services;

/// <summary>
/// Service class for EducationInstitution Model to handle CRUD operations with database
/// </summary>
public class EducationInstitutionModelService : IEducationInstitutionModelService
{
    #region Fields

    private readonly IValidator<EducationInstitutionModel> _validator;
    private readonly IValidator<GetAllEducationInstitutionModelsOptions> _optionsValidator;
    private readonly IEducationInstitutionModelRepository _repository;

    #endregion

    #region Constructors

    public EducationInstitutionModelService(
        IEducationInstitutionModelRepository repository,
        IValidator<EducationInstitutionModel> validator,
        IValidator<GetAllEducationInstitutionModelsOptions> optionsValidator)
    {
        _validator = validator;
        _repository = repository;
        _optionsValidator = optionsValidator;
    }

    #endregion

    public async Task<int> GetNextAvailableId() => await _repository.GetNextAvailableId();

    public async Task<int> GetQueryTotal(GetAllEducationInstitutionModelsOptions options) => await _repository.GetQueryTotal(options);

    public async Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(EducationInstitutionModel model)
    {
        var modelValidationResult = await _validator.ValidateAsync(model);
        if (!modelValidationResult.IsValid) return modelValidationResult.Errors;
        return Enumerable.Empty<ValidationFailure>();

        //check for base newModel parameter uniqueness
        //check for newModel uniqueness

    }

    public async Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllEducationInstitutionModelsOptions options)
    {
        var optionsValidationResult = await _optionsValidator.ValidateAsync(options);
        if (!optionsValidationResult.IsValid) return optionsValidationResult.Errors;
        return Enumerable.Empty<ValidationFailure>();
    }

    public async Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(EducationInstitutionModel model)
    {
        var modelValidationResult = await _validator.ValidateAsync(model);
        if (!modelValidationResult.IsValid) return modelValidationResult.Errors;
        return Enumerable.Empty<ValidationFailure>();

        //check for base newModel parameter uniqueness
        //check for newModel uniqueness

    }

    #region Create

    /// <summary>
    /// Create and insert a new model into database
    /// </summary>
    /// <param name="model">New model parameters</param>
    /// <returns>Created model or the conditions that caused failure</returns>
    public async Task<Fin<int>> CreateAsync(EducationInstitutionModel model, CancellationToken token = default)
    {
        return await _repository.TryCreateAsync(model, token);
    }

    #endregion

    #region Read

    /// <summary>
    /// Retrieve all models from database
    /// </summary>
    /// <returns>A list of all EducationInstitution Models or the fail conditions</returns>
    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllAsync(GetAllEducationInstitutionModelsOptions options, CancellationToken token = default)
    {
        return await _repository.TryGetAllAsync(options, token);
    }

    /// <summary>
    /// Retrieve specific newModel by id from database
    /// </summary>
    /// <param name="id">Id of newModel to retrieve</param>
    /// <returns>Found newModel or the fail condition</returns>
    public async Task<Fin<EducationInstitutionModel>> GetByIDAsync(int id, CancellationToken token = default)
    {
        var model = await _repository.TryGetByIdAsync(id, token);
        if(((EducationInstitutionModel)model).IsNull()) return Error.New(new NullReferenceException());
        return model;
    }

    #endregion

    #region Update

    /// <summary>
    /// Update newModel of id in the database
    /// </summary>
    /// <returns>The updated newModel or the fail condition</returns>d
    public async Task<Fin<int>> UpdateAsync(EducationInstitutionModel newModel, EducationInstitutionModel oldModel, CancellationToken token = default)
    {
        var result = await _repository.TryUpdateToHiddenAsync(oldModel, token);
        if (result.IsFail) return (Error)result;

        return await _repository.TryUpdateAsync(newModel, token);
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete newModel from database
    /// </summary>
    /// <param name="id">Id of newModel to delete</param>
    /// <returns>The last copy of the newModel of the fail condition</returns>
    public async Task<Fin<EducationInstitutionModel>> DeleteAsync(int id, CancellationToken token = default)
    {
        var lastCopy = await _repository.TryGetByIdAsync(id, token);
        if (((EducationInstitutionModel)lastCopy).IsNull()) return Error.New(new NullReferenceException());
        if (lastCopy.IsFail) return (Error)lastCopy;

        var result = await _repository.TryDeleteAsync(id, token);
        if (result.IsFail) return (Error)result;

        return (int)result > 0 ? lastCopy : Error.New("Idk what happened here");
    }

    #endregion
}