using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface ITechTagModelRepository
{
    Task<int> GetNextAvailableId();

    Task<Fin<int>> TryCreateAsync(TechTagModel model, CancellationToken token = default);
    Task<int> CreateAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<TechTagModel>>> TryGetAllAsync(GetAllTechTagModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<TechTagModel>> GetAllAsync(GetAllTechTagModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<TechTagModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<TechTagModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(TechTagModel model, CancellationToken token = default);
    Task<int> UpdateAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(TechTagModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}