using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IRepository<T> where T : BaseModel
{
    Task<int> GetNextAvailableId();

    Task<Fin<int>> TryCreateAsync(T model, CancellationToken token = default);
    Task<int> CreateAsync(T model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<T>>> TryGetAllAsync(CancellationToken token = default);
    Task<IEnumerable<T>> GetAllAsync(ResumeContext ctx, CancellationToken token = default);

    Task<Fin<T>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<T> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(T model, CancellationToken token = default);
    Task<int> UpdateAsync(T model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(T model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(T model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}