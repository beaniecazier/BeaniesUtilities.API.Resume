using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface ICertificateModelService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(CertificateModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(CertificateModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllCertificateModelsOptions options);

    Task<Fin<IEnumerable<CertificateModel>>> GetAllAsync(GetAllCertificateModelsOptions options, CancellationToken token = default);
    Task<Fin<CertificateModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(CertificateModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(CertificateModel request, CertificateModel oldModel, CancellationToken token = default);

    Task<Fin<CertificateModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591