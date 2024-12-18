using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class TechTagModelInMemRepository : ITechTagModelRepository
{
    List<TechTagModel> _techTags = new();

    public Task<int> GetNextAvailableId()
    {
        int lastId = -1;
        if (_techTags.Count > 0)
        {
            lastId = _techTags.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId + 1);
    }

    public async Task<Fin<int>> TryCreateAsync(TechTagModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _techTags.Count == 0 ? 0 : _techTags.Count;
        _techTags.Add(new TechTagModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> TryGetAllAsync(GetAllTechTagModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<TechTagModel>> GetAllAsync(GetAllTechTagModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _techTags.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<TechTagModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<TechTagModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _techTags.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(TechTagModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _techTags.Count == 0 ? 0 : _techTags.Count;
        _techTags.Add(new TechTagModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(TechTagModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new TechTagModel(model, "Tiabeanie", isHidden: true);
        _techTags.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var deletions = _techTags.Where(x => x.CommonIdentity == id).Select(x => new TechTagModel(x, "Tiabeanie", isDeleted: true));
        _techTags = _techTags.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
