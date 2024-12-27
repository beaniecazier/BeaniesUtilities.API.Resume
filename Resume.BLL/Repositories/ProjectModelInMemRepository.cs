using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class ProjectModelInMemRepository : IProjectModelRepository
{
    private readonly object _lock = new object();
    List<ProjectModel> _projects = new();

    public Task<int> GetNextAvailableId()
    {
        if (_projects.Count <= 0) return Task.FromResult(0);
        int lastId = -1;

        lock (_lock)
        {
            lastId = _projects.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId+1);
    }

    public async Task<Fin<int>> TryCreateAsync(ProjectModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(ProjectModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _projects.Count == 0 ? 0 : _projects.Count;
        _projects.Add(new ProjectModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<ProjectModel>>> TryGetAllAsync(GetAllProjectModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<ProjectModel>> GetAllAsync(GetAllProjectModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _projects.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<ProjectModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<ProjectModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _projects.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(ProjectModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(ProjectModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _projects.Count == 0 ? 0 : _projects.Count;
        _projects.Add(new ProjectModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(ProjectModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(ProjectModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new ProjectModel(model, "Tiabeanie", isHidden: true);
        _projects.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        _projects = _projects.Select(x => (x.CommonIdentity == id ? new ProjectModel(x, "Tiabeanie", isDeleted: true) : x)).ToList();
        //var deletions = _projects.Where(x => x.CommonIdentity == id).Select(x => new ProjectModel(x, "Tiabeanie", isDeleted: true));
        //_projects = _projects.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
