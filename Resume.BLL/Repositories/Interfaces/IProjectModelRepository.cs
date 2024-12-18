using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IProjectModelRepository
{
    Task<int> GetNextAvailableId();

    Task<Fin<int>> TryCreateAsync(ProjectModel model, CancellationToken token = default);
    Task<int> CreateAsync(ProjectModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<ProjectModel>>> TryGetAllAsync(GetAllProjectModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<ProjectModel>> GetAllAsync(GetAllProjectModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<ProjectModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<ProjectModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(ProjectModel model, CancellationToken token = default);
    Task<int> UpdateAsync(ProjectModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(ProjectModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(ProjectModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}