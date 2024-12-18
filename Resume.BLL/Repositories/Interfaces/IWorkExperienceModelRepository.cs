using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IWorkExperienceModelRepository
{
    Task<int> GetNextAvailableId();

    Task<Fin<int>> TryCreateAsync(WorkExperienceModel model, CancellationToken token = default);
    Task<int> CreateAsync(WorkExperienceModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<WorkExperienceModel>>> TryGetAllAsync(GetAllWorkExperienceModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<WorkExperienceModel>> GetAllAsync(GetAllWorkExperienceModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<WorkExperienceModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<WorkExperienceModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(WorkExperienceModel model, CancellationToken token = default);
    Task<int> UpdateAsync(WorkExperienceModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(WorkExperienceModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(WorkExperienceModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}