using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Net;
using LanguageExt.Common;
using Gay.TCazier.DatabaseParser.Models.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Gay.TCazier.DatabaseParser.Services;

public class ResumeService : BaseModelService, IResumeService
{
    #region Fields

    IServiceProvider _provider;

    #endregion

    #region Constructors

    public ResumeService(IServiceProvider provider)
    {
        _provider = provider;
    }

    #endregion

    #region Create

    /// <summary>
    /// 
    /// </summary>
    /// <param name="editibleAttributes"></param>
    /// <returns></returns>
    public async Task<Fin<ResumeModel>> CreateAsync(ResumeContext ctx, EditibleResumeModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var entries = await ctx.People
            .Where(x => x.GetType() == typeof(ResumeModel))
			.Select(x => x as ResumeModel)
			.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness
        int id = GetNextCommonID(entries);
        var model = new ResumeModel();
        model.Create(id, editibleAttributes, "Tiabeanie");

        await ctx.People.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    #endregion

    #region Read

    public async Task<Fin<IEnumerable<ResumeModel>>> GetAllAsync(ResumeContext ctx)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var entries = await ctx.People
            .Where(x => x.GetType() == typeof(ResumeModel))
			.Select(x => x as ResumeModel)
			.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var entries = await ctx.People
            .Where(x => x.EntryIdentity >= start &&
                        x.EntryIdentity <= end)
            .Where(x => x.GetType() == typeof(ResumeModel))
			.Select(x => x as ResumeModel)
			.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var entries = await ctx.People
            .Where(x => x.CommonIdentity >= start &&
                        x.CommonIdentity <= end &&
                        !x.IsHidden)
            .Where(x => x.GetType() == typeof(ResumeModel))
			.Select(x => x as ResumeModel)
			.ToListAsync();
        return entries;
    }

    public async Task<Fin<ResumeModel>> GetByEntryIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        try
        {
            var entry = await ctx.People
                .Where(x => x.EntryIdentity == id)
                .Select(x => x as ResumeModel)
			.SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    public async Task<Fin<ResumeModel>> GetByIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        try
        {
            var entry = await ctx.People
                .Where(x => x.CommonIdentity == id)
                .Select(x => x as ResumeModel)
			.SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    //public async Task<Fin<IEnumerable<ResumeModel>>> GetHistroyOfIDAsync(ResumeContext ctx, int id)
    //{
    //    if (ctx == null)
    //    {
    //        return Error.New(
    //            new NullReferenceException(
    //                "The provider returned a null DbContext while trying to create a new Resume model"
    //                ));
    //    }

    //    var entries = await ctx.People
    //        .Where(x => x.EntryIdentity == id)
    //        .Where(x => x.GetType() == typeof(ResumeModel))
	//		  .Select(x => x as ResumeModel)
	//		  .ToListAsync();
    //    return entries;
    //}

    #endregion

    #region Query

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        throw new NotImplementedException();
    }

    #endregion

    #region Update

    public async Task<Fin<ResumeModel>> UpdateAsync(ResumeContext ctx, int id, EditibleResumeModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var existingEntry = await ctx.People
            .Where(x => x.CommonIdentity == id &&
                !x.IsHidden)
            .Select(x => x as ResumeModel)
			.SingleOrDefaultAsync();

        if (existingEntry is null)
        {
            return Error.New(
                new NullReferenceException(
                    "There is no entry in the database with that id"
                    ));
        }

        var updatedEntry = existingEntry.Update(editibleAttributes, "Tiabeanie");

        await ctx.People.AddAsync(updatedEntry);
        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion

    #region Delete

    public async Task<Fin<IEnumerable<ResumeModel>>> DeleteAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var existingEntry = await ctx.People
            .Where(x => x.CommonIdentity == id)
            .Where(x => x.GetType() == typeof(ResumeModel))
			.Select(x => x as ResumeModel)
			.ToListAsync();

        if (existingEntry.IsNullOrEmpty()) return Error.New($"404 - No entries with id {id}");
            
        existingEntry = existingEntry.Where(x => !x.IsDeleted).ToList();

        if (existingEntry.IsNullOrEmpty()) return Error.New("204 - Nothing to delete");

        foreach (var entry in existingEntry) entry.IsDeleted = true;

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    public async Task<Fin<ResumeModel>> DeleteEntryAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Resume model"
                    ));
        }

        var existingEntry = await ctx.People
            .Where(x => x.CommonIdentity == id &&
                (!x.IsDeleted))
            .Select(x => x as ResumeModel)
			.SingleOrDefaultAsync();

        if (existingEntry is null) return existingEntry;

        existingEntry.IsDeleted = true;

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion
}