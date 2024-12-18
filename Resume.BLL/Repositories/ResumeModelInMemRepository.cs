using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class ResumeModelInMemRepository : IResumeModelRepository
{
    List<ResumeModel> _resumes = new();

    public Task<int> GetNextAvailableId()
    {
        int lastId = -1;
        if (_resumes.Count > 0)
        {
            lastId = _resumes.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId + 1);
    }

    public async Task<Fin<int>> TryCreateAsync(ResumeModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _resumes.Count == 0 ? 0 : _resumes.Count;
        _resumes.Add(new ResumeModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> TryGetAllAsync(GetAllResumeModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<ResumeModel>> GetAllAsync(GetAllResumeModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _resumes.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<ResumeModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<ResumeModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _resumes.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(ResumeModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _resumes.Count == 0 ? 0 : _resumes.Count;
        _resumes.Add(new ResumeModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(ResumeModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new ResumeModel(model, "Tiabeanie", isHidden: true);
        _resumes.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var deletions = _resumes.Where(x => x.CommonIdentity == id).Select(x => new ResumeModel(x, "Tiabeanie", isDeleted: true));
        _resumes = _resumes.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
