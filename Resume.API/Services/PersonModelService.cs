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

public class PersonService : BaseModelService, IPersonService
{
    #region Fields

    IServiceProvider _provider;

    #endregion

    #region Constructors

    public PersonService(IServiceProvider provider)
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
    public async Task<Fin<PersonModel>> CreateAsync(ResumeContext ctx, EditiblePersonModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var entries = await ctx.People
            .ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness
        int id = GetNextCommonID(entries);
        var model = new PersonModel();
        model.Create(id, editibleAttributes, "Tiabeanie");

        await ctx.People.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    #endregion

    #region Read

    public async Task<Fin<IEnumerable<PersonModel>>> GetAllAsync(ResumeContext ctx)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var entries = await ctx.People
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<PersonModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var entries = await ctx.People
            .Where(x => x.EntryIdentity >= start &&
                        x.EntryIdentity <= end)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<PersonModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var entries = await ctx.People
            .Where(x => x.CommonIdentity >= start &&
                        x.CommonIdentity <= end &&
                        !x.IsHidden)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<PersonModel>> GetByEntryIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        try
        {
            var entry = await ctx.People
                .Where(x => x.EntryIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    public async Task<Fin<PersonModel>> GetByIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        try
        {
            var entry = await ctx.People
                .Where(x => x.CommonIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    //public async Task<Fin<IEnumerable<PersonModel>>> GetHistroyOfIDAsync(ResumeContext ctx, int id)
    //{
    //    if (ctx == null)
    //    {
    //        return Error.New(
    //            new NullReferenceException(
    //                "The provider returned a null DbContext while trying to create a new Person model"
    //                ));
    //    }

    //    var entries = await ctx.People
    //        .Where(x => x.EntryIdentity == id)
    //        .ToListAsync();
    //    return entries;
    //}

    #endregion

    #region Query

    public async Task<Fin<IEnumerable<PersonModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PersonModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PersonModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PersonModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PersonModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        throw new NotImplementedException();
    }

    #endregion

    #region Update

    public async Task<Fin<PersonModel>> UpdateAsync(ResumeContext ctx, int id, EditiblePersonModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var existingEntry = await ctx.People
            .Where(x => x.CommonIdentity == id &&
                !x.IsHidden)
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

    public async Task<Fin<IEnumerable<PersonModel>>> DeleteAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var existingEntry = await ctx.People
            .Where(x => x.CommonIdentity == id)
            .ToListAsync();

        if (existingEntry.IsNullOrEmpty()) return Error.New($"404 - No entries with id {id}");
            
        existingEntry = existingEntry.Where(x => !x.IsDeleted).ToList();

        if (existingEntry.IsNullOrEmpty()) return Error.New("204 - Nothing to delete");

        foreach (var entry in existingEntry) entry.IsDeleted = true;

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    public async Task<Fin<PersonModel>> DeleteEntryAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Person model"
                    ));
        }

        var existingEntry = await ctx.People
            .Where(x => x.CommonIdentity == id &&
                (!x.IsDeleted))
            .SingleOrDefaultAsync();

        if (existingEntry is null) return existingEntry;

        existingEntry.IsDeleted = true;

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion
}