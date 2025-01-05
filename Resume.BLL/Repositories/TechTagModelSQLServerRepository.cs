using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;

using LanguageExt;
using LanguageExt.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class TechTagModelSQLServerRepository : ITechTagModelRepository
{
    private readonly IServiceProvider _serviceProvider;
    private readonly object _lock = new object();

    private List<int> _commonIdTickets = new List<int>();
    
    public TechTagModelSQLServerRepository(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<int> GetNextAvailableId()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<ResumeContext>();
            return GetNextTicket(ctx);
        }
    }

    private int GetNextTicket(ResumeContext ctx)
    {
        int nextId = 0;
        lock (_lock)
        {
            var models = ctx.TechTags.IgnoreQueryFilters();

            if (!models.Any()) nextId = 0;
            else if (!_commonIdTickets.Any()) nextId = models.Max(x => x.CommonIdentity) + 1;
            else nextId = _commonIdTickets.Max() + 1;

            _commonIdTickets.Add(nextId);
        }
        return nextId;
    }

    public async Task<int> GetQueryTotal(GetAllTechTagModelsOptions options)
    {
        using(var scope = _serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<ResumeContext>();
            if(!ctx.TechTags.Any()) return 0;
            int count = ctx.TechTags.GroupBy(x=>x.CommonIdentity).Count();
            return count;
        }
    }

    private Fin<ResumeContext> ValidateContext(IServiceScope scope)
    {
        var ctx = scope.ServiceProvider.GetRequiredService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to perform an TechTag Model Service"));
        }
        if (ctx.TechTags is null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider DbSet of TechTag Models was NULL or uninitialized"
            ));
        }
        return ctx;
    }

    public async Task<Fin<int>> TryCreateAsync(TechTagModel model, CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return await CreateAsync(model, ctx, token);
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<int> CreateAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default)
    {

        await ctx.TechTags.AddAsync(model);
        int count = await ctx.SaveChangesAsync();
        if (count > 0) _commonIdTickets.Remove(model.CommonIdentity);
        return count;
    }
    
    public async Task<Fin<IEnumerable<TechTagModel>>> TryGetAllAsync(GetAllTechTagModelsOptions options, CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return (await GetAllAsync(options, ctx, token)).ToList();
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<IEnumerable<TechTagModel>> GetAllAsync(GetAllTechTagModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var allDBEntries = (await ctx.TechTags.ToListAsync()).GroupByAndFindLatest();

        if (options.SortOrder == SortOrder.Ascending)
        {
            allDBEntries = allDBEntries.OrderBy(x => options.SortField);
        }
        else if (options.SortOrder == SortOrder.Descending)
        {
            allDBEntries = allDBEntries.OrderByDescending(x => options.SortField);
        }

        if (!options.HasFilters) return allDBEntries.Paginate(options.PageIndex, options.PageSize);

        allDBEntries = allDBEntries.FilterByIdRange(options.GreaterThanOrEqualToID, options.LessThanOrEqualToID)
                        .FilterByModifiedDate(options.BeforeDate, options.AfterDate)
                        .FilterName(options.NameSearchTerm!)
                        .FilterNotes(options.NotesSearchTerm!)
                        .Paginate(options.PageIndex, options.PageSize);

        return allDBEntries;
    }

    public async Task<Fin<TechTagModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return await GetByIdAsync(id, ctx, token);
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<TechTagModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var entries = (await ctx.TechTags.ToListAsync()).GroupByAndFindLatest();
        var entry = entries.Where(x => x.CommonIdentity == id).SingleOrDefault();
        return entry!;
    }

    public async Task<Fin<int>> TryUpdateAsync(TechTagModel model, CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return await UpdateAsync(model, ctx, token);
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<int> UpdateAsync(TechTagModel model, ResumeContext ctx, CancellationToken token = default)
    {
        await ctx.TechTags.AddAsync(model);
        return await ctx.SaveChangesAsync();
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(TechTagModel model, CancellationToken token)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return await UpdateToHiddenAsync(model, ctx, token);
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<int> UpdateToHiddenAsync(TechTagModel oldModel, ResumeContext ctx, CancellationToken token)
    {
        return await ctx.TechTags
                        .Where(x => x.EntryIdentity == oldModel.EntryIdentity)
                        .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsHidden, true)
                                                    .SetProperty(p => p.HiddenOn, DateTime.UtcNow)
                                                    .SetProperty(p => p.HiddenBy, "Tiabeanie"),
                                            token);
    }

    public async Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return await DeleteAsync(id, ctx, token);
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        return await ctx.TechTags
            .IgnoreQueryFilters()
            .Where(x => x.CommonIdentity == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true)
                                    .SetProperty(p => p.DeletedBy, "'Tiabeanie")
                                    .SetProperty(p => p.DeletedOn, DateTime.UtcNow));
    }

}