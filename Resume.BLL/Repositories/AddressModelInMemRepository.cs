using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class AddressModelInMemRepository : IAddressModelRepository
{
    private readonly object _lock = new object();
    List<AddressModel> _addresses = new();

    public Task<int> GetNextAvailableId()
    {
        if (_addresses.Count <= 0) return Task.FromResult(0);
        int lastId = -1;

        lock (_lock)
        {
            lastId = _addresses.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId+1);
    }

    public async Task<Fin<int>> TryCreateAsync(AddressModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _addresses.Count == 0 ? 0 : _addresses.Count;
        _addresses.Add(new AddressModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<AddressModel>>> TryGetAllAsync(GetAllAddressModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<AddressModel>> GetAllAsync(GetAllAddressModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _addresses.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<AddressModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<AddressModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _addresses.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(AddressModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _addresses.Count == 0 ? 0 : _addresses.Count;
        _addresses.Add(new AddressModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(AddressModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new AddressModel(model, "Tiabeanie", isHidden: true);
        _addresses.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        _addresses = _addresses.Select(x => (x.CommonIdentity == id ? new AddressModel(x, "Tiabeanie", isDeleted: true) : x)).ToList();
        //var deletions = _addresses.Where(x => x.CommonIdentity == id).Select(x => new AddressModel(x, "Tiabeanie", isDeleted: true));
        //_addresses = _addresses.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
