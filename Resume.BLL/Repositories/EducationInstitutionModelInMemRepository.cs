using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class EducationInstitutionModelInMemRepository : IEducationInstitutionModelRepository
{
    private readonly object _lock = new object();
    List<EducationInstitutionModel> _educationInstitutions = new();

    public Task<int> GetNextAvailableId()
    {
        if (_educationInstitutions.Count <= 0) return Task.FromResult(0);
        int lastId = -1;

        lock (_lock)
        {
            lastId = _educationInstitutions.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId+1);
    }

    public async Task<int> GetQueryTotal(GetAllEducationInstitutionModelsOptions options)
    {
        if ( _educationInstitutions.Count <= 0) return 0;
        int count = _educationInstitutions.GroupBy(x => x.CommonIdentity).Count();
        return count;
    }

    public async Task<Fin<int>> TryCreateAsync(EducationInstitutionModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(EducationInstitutionModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _educationInstitutions.Count == 0 ? 0 : _educationInstitutions.Count;
        _educationInstitutions.Add(new EducationInstitutionModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> TryGetAllAsync(GetAllEducationInstitutionModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<EducationInstitutionModel>> GetAllAsync(GetAllEducationInstitutionModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _educationInstitutions.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

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

    public async Task<Fin<EducationInstitutionModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<EducationInstitutionModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _educationInstitutions.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(EducationInstitutionModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(EducationInstitutionModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _educationInstitutions.Count == 0 ? 0 : _educationInstitutions.Count;
        _educationInstitutions.Add(new EducationInstitutionModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(EducationInstitutionModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(EducationInstitutionModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new EducationInstitutionModel(model, "Tiabeanie", isHidden: true);
        _educationInstitutions.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        _educationInstitutions = _educationInstitutions.Select(x => (x.CommonIdentity == id ? new EducationInstitutionModel(x, "Tiabeanie", isDeleted: true) : x)).ToList();
        //var deletions = _educationInstitutions.Where(x => x.CommonIdentity == id).Select(x => new EducationInstitutionModel(x, "Tiabeanie", isDeleted: true));
        //_educationInstitutions = _educationInstitutions.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
