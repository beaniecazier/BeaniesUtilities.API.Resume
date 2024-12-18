using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class EducationDegreeModelInMemRepository : IEducationDegreeModelRepository
{
    List<EducationDegreeModel> _educationDegrees = new();

    public Task<int> GetNextAvailableId()
    {
        int lastId = -1;
        if (_educationDegrees.Count > 0)
        {
            lastId = _educationDegrees.GroupByAndFindLatest().Max(x => x.CommonIdentity);
        }
        return Task.FromResult(lastId + 1);
    }

    public async Task<Fin<int>> TryCreateAsync(EducationDegreeModel model, CancellationToken token = default)
    {
        try { return await CreateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> CreateAsync(EducationDegreeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _educationDegrees.Count == 0 ? 0 : _educationDegrees.Count;
        _educationDegrees.Add(new EducationDegreeModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> TryGetAllAsync(GetAllEducationDegreeModelsOptions options, CancellationToken token = default)
    {
        try { return (await GetAllAsync(options, null, token)).ToList(); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<IEnumerable<EducationDegreeModel>> GetAllAsync(GetAllEducationDegreeModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var models = _educationDegrees.GroupByAndFindLatest(options.AllowHidden??false, options.AllowDeleted??false);

        if(!options.HasFilters) return Task.FromResult(models);

        models = models.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!);

        return Task.FromResult(models);
    }

    public async Task<Fin<EducationDegreeModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try { return await GetByIdAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<EducationDegreeModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var model = _educationDegrees.GroupByAndFindLatest()
            .Where(x => x.CommonIdentity == id && !x.IsHidden && !x.IsDeleted)
            .SingleOrDefault();
        return Task.FromResult(model);
    }

    public async Task<Fin<int>> TryUpdateAsync(EducationDegreeModel model, CancellationToken token = default)
    {
        try { return await UpdateAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateAsync(EducationDegreeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        int nextSpot = _educationDegrees.Count == 0 ? 0 : _educationDegrees.Count;
        _educationDegrees.Add(new EducationDegreeModel(model, "Tiabeanie", entryId: nextSpot));
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(EducationDegreeModel model, CancellationToken token = default)
    {
        try { return await UpdateToHiddenAsync(model, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> UpdateToHiddenAsync(EducationDegreeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        var adjustedEntry = new EducationDegreeModel(model, "Tiabeanie", isHidden: true);
        _educationDegrees.Replace(model.EntryIdentity, adjustedEntry);
        return Task.FromResult(1);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try { return await DeleteAsync(id, null, token); }
        catch (Exception ex) { return Error.New(ex); }
    }

    public Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var deletions = _educationDegrees.Where(x => x.CommonIdentity == id).Select(x => new EducationDegreeModel(x, "Tiabeanie", isDeleted: true));
        _educationDegrees = _educationDegrees.Replace(deletions).ToList();
        return Task.FromResult(1);
    }
}
