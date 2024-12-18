using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class PhoneNumberModelInMemRepository : IPhoneNumberModelRepository
{
    List<PhoneNumberModel> _phoneNumbers = new();

    public Task<int> GetNextAvailableId()
    {
        int lastId = -1;
        if (_phoneNumbers.Count > 0)
        {
            lastId = _phoneNumbers.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId + 1);
    }

    public async Task<Fin<int>> TryCreateAsync(PhoneNumberModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(PhoneNumberModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _phoneNumbers.Count == 0 ? 0 : _phoneNumbers.Count;
        _phoneNumbers.Add(new PhoneNumberModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> TryGetAllAsync(GetAllPhoneNumberModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<PhoneNumberModel>> GetAllAsync(GetAllPhoneNumberModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _phoneNumbers.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<PhoneNumberModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<PhoneNumberModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _phoneNumbers.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(PhoneNumberModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(PhoneNumberModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _phoneNumbers.Count == 0 ? 0 : _phoneNumbers.Count;
        _phoneNumbers.Add(new PhoneNumberModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(PhoneNumberModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(PhoneNumberModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new PhoneNumberModel(model, "Tiabeanie", isHidden: true);
        _phoneNumbers.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var deletions = _phoneNumbers.Where(x => x.CommonIdentity == id).Select(x => new PhoneNumberModel(x, "Tiabeanie", isDeleted: true));
        _phoneNumbers = _phoneNumbers.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
