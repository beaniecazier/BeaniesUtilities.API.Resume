using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class WorkExperienceModelInMemRepository : IWorkExperienceModelRepository
{
    private readonly object _lock = new object();
    List<WorkExperienceModel> _workExperiences = new();

    public Task<int> GetNextAvailableId()
    {
        if (_workExperiences.Count <= 0) return Task.FromResult(0);
        int lastId = -1;

        lock (_lock)
        {
            lastId = _workExperiences.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId+1);
    }

    public async Task<Fin<int>> TryCreateAsync(WorkExperienceModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(WorkExperienceModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _workExperiences.Count == 0 ? 0 : _workExperiences.Count;
        _workExperiences.Add(new WorkExperienceModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> TryGetAllAsync(GetAllWorkExperienceModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<WorkExperienceModel>> GetAllAsync(GetAllWorkExperienceModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _workExperiences.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<WorkExperienceModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<WorkExperienceModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _workExperiences.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(WorkExperienceModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(WorkExperienceModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _workExperiences.Count == 0 ? 0 : _workExperiences.Count;
        _workExperiences.Add(new WorkExperienceModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(WorkExperienceModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(WorkExperienceModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new WorkExperienceModel(model, "Tiabeanie", isHidden: true);
        _workExperiences.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        _workExperiences = _workExperiences.Select(x => (x.CommonIdentity == id ? new WorkExperienceModel(x, "Tiabeanie", isDeleted: true) : x)).ToList();
        //var deletions = _workExperiences.Where(x => x.CommonIdentity == id).Select(x => new WorkExperienceModel(x, "Tiabeanie", isDeleted: true));
        //_workExperiences = _workExperiences.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
