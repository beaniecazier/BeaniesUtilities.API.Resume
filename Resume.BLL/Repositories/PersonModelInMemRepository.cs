using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class PersonModelInMemRepository : IPersonModelRepository
{
    private readonly object _lock = new object();
    List<PersonModel> _People = new();

    public Task<int> GetNextAvailableId()
    {
        if (_People.Count <= 0) return Task.FromResult(0);
        int lastId = -1;

        lock (_lock)
        {
            lastId = _People.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId+1);
    }

    public async Task<int> GetQueryTotal(GetAllPersonModelsOptions options)
    {
        if ( _People.Count <= 0) return 0;
        int count = _People.GroupBy(x => x.CommonIdentity).Count();
        return count;
    }

    public async Task<Fin<int>> TryCreateAsync(PersonModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _People.Count == 0 ? 0 : _People.Count;
        _People.Add(new PersonModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<PersonModel>>> TryGetAllAsync(GetAllPersonModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<PersonModel>> GetAllAsync(GetAllPersonModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _People.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(options.SortOrder == SortOrder.Ascending)
        {
            models = models.OrderBy(x=>options.SortField);
        }
        else if(options.SortOrder == SortOrder.Descending)
        {
            models = models.OrderByDescending(x=>options.SortField);
        }

        if(!options.HasFilters) return Task.FromResult(models.Paginate(options.PageIndex,options.PageSize));

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!)
                        .Paginate(options.PageIndex,options.PageSize);

        return Task.FromResult(models);
    }

    public async Task<Fin<PersonModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<PersonModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _People.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(PersonModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _People.Count == 0 ? 0 : _People.Count;
        _People.Add(new PersonModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(PersonModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new PersonModel(model, "Tiabeanie", isHidden: true);
        _People.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        _People = _People.Select(x => (x.CommonIdentity == id ? new PersonModel(x, "Tiabeanie", isDeleted: true) : x)).ToList();
        //var deletions = _People.Where(x => x.CommonIdentity == id).Select(x => new PersonModel(x, "Tiabeanie", isDeleted: true));
        //_People = _People.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
