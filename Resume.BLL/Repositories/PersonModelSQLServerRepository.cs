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

public class PersonModelSQLServerRepository : IPersonModelRepository
{
    private readonly IServiceProvider _serviceProvider;
    private readonly object _lock = new object();

    private List<int> _commonIdTickets = new List<int>();
    
    public PersonModelSQLServerRepository(IServiceProvider serviceProvider)
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

    public async Task<int> GetQueryTotal(GetAllPersonModelsOptions options)
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
                    "The provider returned a null DbContext while trying to perform an Person Model Service"));
        }
        if (ctx.People is null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider DbSet of Person Models was NULL or uninitialized"
            ));
        }
        return ctx;
    }

    public async Task<Fin<int>> TryCreateAsync(PersonModel model, CancellationToken token = default)
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

    public async Task<int> CreateAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default)
    {
		foreach(var item in model.Addresses) ctx.Attach(item);
		foreach(var item in model.PhoneNumbers) ctx.Attach(item);
        await ctx.People.AddAsync(model);
        int count = await ctx.SaveChangesAsync();
        if (count > 0) _commonIdTickets.Remove(model.CommonIdentity);
        return count;
    }
    
    public async Task<Fin<IEnumerable<PersonModel>>> TryGetAllAsync(GetAllPersonModelsOptions options, CancellationToken token = default)
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

    public async Task<IEnumerable<PersonModel>> GetAllAsync(GetAllPersonModelsOptions options, ResumeContext ctx, CancellationToken token = default)
    {
        var allDBEntries = (await ctx.People.ToListAsync()).GroupByAndFindLatest();

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

    public async Task<Fin<PersonModel>> TryGetByIdAsync(int id, CancellationToken token = default)
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

    public async Task<PersonModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var entries = (await ctx.People.ToListAsync()).GroupByAndFindLatest();
        var entry = entries.Where(x => x.CommonIdentity == id).SingleOrDefault();
        return entry!;
    }

    public async Task<Fin<int>> TryUpdateAsync(PersonModel model, CancellationToken token = default)
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

    public async Task<int> UpdateAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default)
    {
        await ctx.People.AddAsync(model);
        return await ctx.SaveChangesAsync();
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(PersonModel model, CancellationToken token)
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

    public async Task<int> UpdateToHiddenAsync(PersonModel oldModel, ResumeContext ctx, CancellationToken token)
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