using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class AddressModelInMemRepository : IRepository<AddressModel>
{
    List<AddressModel> _addresses = new();

    public Task<int> GetNextAvailableId()
    {
        int lastId = -1;
        if(_addresses.Count > 0)
        {
            lastId = _addresses.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId + 1);
    }

    public async Task<Fin<int>> TryCreateAsync(AddressModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _addresses.Count == 0 ? 0 : _addresses.Count;
        _addresses.Add(new AddressModel(model, "Tiabeanie", entryId:nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<AddressModel>>> TryGetAllAsync(CancellationToken token = default)
    {
        try { return (await GetAllAsync(null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<AddressModel>> GetAllAsync(ResumeContext ctx, CancellationToken token = default)
    {
        return Task.FromResult(_addresses.GroupByAndFindLatest());
    }

    public async Task<Fin<AddressModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<AddressModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _addresses.GroupByAndFindLatest()
            .Where(x=>x.CommonIdentity==id)
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
        var deletions = _addresses.Where(x=>x.CommonIdentity == id).Select(x=>new AddressModel(x, "Tiabeanie", isDeleted:true));
        _addresses = _addresses.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
