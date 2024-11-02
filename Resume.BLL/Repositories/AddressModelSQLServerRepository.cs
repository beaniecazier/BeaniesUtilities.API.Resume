using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Database;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gay.TCazier.Resume.BLL.Repositories;

public class AddressModelSQLServerRepository : IRepository<AddressModel>
{
    private readonly IServiceProvider _serviceProvider;

    public AddressModelSQLServerRepository(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<int> GetNextAvailableId()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<ResumeContext>();
            return !ctx.Addresses.Any() ? 0 : ctx.Addresses.Max(x => x.CommonIdentity) + 1;
        }
    }

    private Fin<ResumeContext> ValidateContext(IServiceScope scope)
    {
        var ctx = scope.ServiceProvider.GetRequiredService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to perform an Address Model Service"));
        }
        if (ctx.Addresses is null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider DbSet of Address Models was NULL or uninitialized"
            ));
        }
        return ctx;
    }

    public async Task<Fin<int>> TryCreateAsync(AddressModel model, CancellationToken token = default)
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

    public async Task<int> CreateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default)
    {
        await ctx.Addresses.AddAsync(model);
        await ctx.SaveChangesAsync();
        return model.CommonIdentity;
    }

    public async Task<Fin<IEnumerable<AddressModel>>> TryGetAllAsync(CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;
                return (await GetAllAsync(ctx, token)).ToList();
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<IEnumerable<AddressModel>> GetAllAsync(ResumeContext ctx, CancellationToken token = default)
    {
        var allDBEntries = await ctx.Addresses.ToListAsync();
        return allDBEntries.GroupByAndFindLatest();
    }

    public async Task<Fin<AddressModel>> TryGetByIdAsync(int id, CancellationToken token = default)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var contextValidationResult = ValidateContext(scope);
                if (contextValidationResult.IsFail) return Error.New((Error)contextValidationResult);

                var ctx = (ResumeContext)contextValidationResult;

                var model = await GetByIdAsync(id, ctx, token);
                if (model is null)
                {
                    return Error.New(new NullReferenceException($"The repository returned a null while attempting to fetch Address Model with ID:{id}"));
                }
                return model;
            }
        }
        catch (Exception e) { return Error.New(e); }
    }

    public async Task<AddressModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default)
    {
        var entries = (await ctx.Addresses.ToListAsync()).GroupByAndFindLatest();
        var entry = entries.Where(x => x.CommonIdentity == id).SingleOrDefault();
        return entry;
    }

    public async Task<Fin<int>> TryUpdateAsync(AddressModel model, CancellationToken token = default)
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

    public async Task<int> UpdateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default)
    {
        await ctx.Addresses.AddAsync(model);
        return await ctx.SaveChangesAsync();
    }

    public async Task<Fin<int>> TryUpdateToHiddenAsync(AddressModel model, CancellationToken token)
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

    public async Task<int> UpdateToHiddenAsync(AddressModel oldModel, ResumeContext ctx, CancellationToken token)
    {
        return await ctx.Addresses
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
        return await ctx.Addresses
            .IgnoreQueryFilters()
            .Where(x => x.CommonIdentity == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true)
                                    .SetProperty(p => p.DeletedBy, "'Tiabeanie")
                                    .SetProperty(p => p.DeletedOn, DateTime.UtcNow));
    }

}
