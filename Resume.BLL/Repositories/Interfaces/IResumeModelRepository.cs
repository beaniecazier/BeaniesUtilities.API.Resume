using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IResumeModelRepository
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllResumeModelsOptions options);

    Task<Fin<int>> TryCreateAsync(ResumeModel model, CancellationToken token = default);
    Task<int> CreateAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<ResumeModel>>> TryGetAllAsync(GetAllResumeModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<ResumeModel>> GetAllAsync(GetAllResumeModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<ResumeModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<ResumeModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(ResumeModel model, CancellationToken token = default);
    Task<int> UpdateAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(ResumeModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}