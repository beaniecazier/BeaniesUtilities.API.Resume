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

public class ResumeModelSQLServerRepository : IResumeModelRepository
{
    private readonly IServiceProvider _serviceProvider;
    private readonly object _lock = new object();

    private List<int> _commonIdTickets = new List<int>();
    
    public ResumeModelSQLServerRepository(IServiceProvider serviceProvider)
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
            var models = ctx.People.IgnoreQueryFilters();

            if (!models.Any()) nextId = 0;
            else if (!_commonIdTickets.Any()) nextId = models.Max(x => x.CommonIdentity) + 1;
            else nextId = _commonIdTickets.Max() + 1;

            _commonIdTickets.Add(nextId);
        }
        return nextId;
    }

    public async Task<int> GetQueryTotal(GetAllResumeModelsOptions options)
    {
        using(var scope = _serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<ResumeContext>();
            if(!ctx.People.Any()) return 0;
            int count = ctx.People.GroupBy(x=>x.CommonIdentity).Count();
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
                    "The provider returned a null DbContext while trying to perform an Resume Model Service"));
        }
        if (ctx.People is null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider DbSet of Resume Models was NULL or uninitialized"
            ));
        }
        return ctx;
    }

    public async Task<Fin<int>> TryCreateAsync(ResumeModel model, CancellationToken token = default)
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

    public async Task<int> CreateAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default)
    {
		foreach(var item in model.Degrees) ctx.Attach(item);
		foreach(var item in model.Certificates) ctx.Attach(item);
		foreach(var item in model.WorkExperience) ctx.Attach(item);
		foreach(var item in model.Projects) ctx.Attach(item);
		foreach(var item in model.Addresses) ctx.Attach(item);
		foreach(var item in model.PhoneNumbers) ctx.Attach(item);
        await ctx.People.AddAsync(model);
        int count = await ctx.SaveChangesAsync();
        if (count > 0) _commonIdTickets.Remove(model.CommonIdentity);
        return count;
    }
    
    public async Task<Fin<IEnumerable<ResumeModel>>> TryGetAllAsync(GetAllResumeModelsOptions options, CancellationToken token = default)
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

    public async Task<IEnumerable<ResumeModel>> GetAllAsync(GetAllResumeModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var allDBEntries = (await ctx.People.Where(x => x.GetType() == typeof(ResumeModel)).Cast<ResumeModel>().ToListAsync()).GroupByAndFindLatest();

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

    public async Task<Fin<ResumeModel>> TryGetByIdAsync(int id, CancellationToken token = default)
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

    public async Task<ResumeModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var entries = (await ctx.People.Where(x => x.GetType() == typeof(ResumeModel)).Cast<ResumeModel>().ToListAsync()).GroupByAndFindLatest();
        var entry = entries.Where(x => x.CommonIdentity == id).SingleOrDefault();
        return entry!;
    }

    public async Task<Fin<int>> TryUpdateAsync(ResumeModel model, CancellationToken token = default)
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

    public async Task<int> UpdateAsync(ResumeModel model, ResumeContext ctx, CancellationToken token = default)
    {
        await ctx.People.AddAsync(model);
        return await ctx.SaveChangesAsync();
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(ResumeModel model, CancellationToken token)
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

    public async Task<int> UpdateToHiddenAsync(ResumeModel oldModel, ResumeContext ctx, CancellationToken token)
    {
        return await ctx.People
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
        return await ctx.People
            .IgnoreQueryFilters()
            .Where(x => x.CommonIdentity == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true)
                                    .SetProperty(p => p.DeletedBy, "'Tiabeanie")
                                    .SetProperty(p => p.DeletedOn, DateTime.UtcNow));
    }

}