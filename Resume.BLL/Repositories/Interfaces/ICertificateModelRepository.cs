using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface ICertificateModelRepository
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllCertificateModelsOptions options);

    Task<Fin<int>> TryCreateAsync(CertificateModel model, CancellationToken token = default);
    Task<int> CreateAsync(CertificateModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<CertificateModel>>> TryGetAllAsync(GetAllCertificateModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<CertificateModel>> GetAllAsync(GetAllCertificateModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<CertificateModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<CertificateModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(CertificateModel model, CancellationToken token = default);
    Task<int> UpdateAsync(CertificateModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(CertificateModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(CertificateModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}