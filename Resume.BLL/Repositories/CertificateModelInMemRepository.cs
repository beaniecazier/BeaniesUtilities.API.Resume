using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class CertificateModelInMemRepository : ICertificateModelRepository
{
    private readonly object _lock = new object();
    List<CertificateModel> _certificates = new();

    public Task<int> GetNextAvailableId()
    {
        if (_certificates.Count <= 0) return Task.FromResult(0);
        int lastId = -1;

        lock (_lock)
        {
            lastId = _certificates.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId+1);
    }

    public async Task<int> GetQueryTotal(GetAllCertificateModelsOptions options)
    {
        if ( _certificates.Count <= 0) return 0;
        int count = _certificates.GroupBy(x => x.CommonIdentity).Count();
        return count;
    }

    public async Task<Fin<int>> TryCreateAsync(CertificateModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(CertificateModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _certificates.Count == 0 ? 0 : _certificates.Count;
        _certificates.Add(new CertificateModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> TryGetAllAsync(GetAllCertificateModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<CertificateModel>> GetAllAsync(GetAllCertificateModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        if(!options.HasFilters) return Task.FromResult(_certificates.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false));

        var models = _certificates.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!)
                        .Paginate(options.PageIndex,options.PageSize);

        return Task.FromResult(models);
    }

    public async Task<Fin<CertificateModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<CertificateModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _certificates.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(CertificateModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(CertificateModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _certificates.Count == 0 ? 0 : _certificates.Count;
        _certificates.Add(new CertificateModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(CertificateModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(CertificateModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new CertificateModel(model, "Tiabeanie", isHidden: true);
        _certificates.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        _certificates = _certificates.Select(x => (x.CommonIdentity == id ? new CertificateModel(x, "Tiabeanie", isDeleted: true) : x)).ToList();
        //var deletions = _certificates.Where(x => x.CommonIdentity == id).Select(x => new CertificateModel(x, "Tiabeanie", isDeleted: true));
        //_certificates = _certificates.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
