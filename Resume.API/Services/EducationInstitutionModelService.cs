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

public class EducationInstitutionService : BaseModelService, IEducationInstitutionService
{
    #region Fields

    IServiceProvider _provider;

    #endregion

    #region Constructors

    public EducationInstitutionService(IServiceProvider provider)
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
    public async Task<Fin<EducationInstitutionModel>> CreateAsync(ResumeContext ctx, EditibleEducationInstitutionModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var entries = await ctx.EducationInstitutions
            .ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness
        int id = GetNextCommonID(entries);
        var model = new EducationInstitutionModel();
        model.Create(id, editibleAttributes, "Tiabeanie");

        await ctx.EducationInstitutions.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    #endregion

    #region Read

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllAsync(ResumeContext ctx)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var entries = await ctx.EducationInstitutions
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var entries = await ctx.EducationInstitutions
            .Where(x => x.EntryIdentity >= start &&
                        x.EntryIdentity <= end)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var entries = await ctx.EducationInstitutions
            .Where(x => x.CommonIdentity >= start &&
                        x.CommonIdentity <= end &&
                        !x.IsHidden)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<EducationInstitutionModel>> GetByEntryIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        try
        {
            var entry = await ctx.EducationInstitutions
                .Where(x => x.EntryIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    public async Task<Fin<EducationInstitutionModel>> GetByIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        try
        {
            var entry = await ctx.EducationInstitutions
                .Where(x => x.CommonIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    //public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetHistroyOfIDAsync(ResumeContext ctx, int id)
    //{
    //    if (ctx == null)
    //    {
    //        return Error.New(
    //            new NullReferenceException(
    //                "The provider returned a null DbContext while trying to create a new EducationInstitution model"
    //                ));
    //    }

    //    var entries = await ctx.EducationInstitutions
    //        .Where(x => x.EntryIdentity == id)
    //        .ToListAsync();
    //    return entries;
    //}

    #endregion

    #region Query

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        throw new NotImplementedException();
    }

    #endregion

    #region Update

    public async Task<Fin<EducationInstitutionModel>> UpdateAsync(ResumeContext ctx, int id, EditibleEducationInstitutionModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var existingEntry = await ctx.EducationInstitutions
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

        await ctx.EducationInstitutions.AddAsync(updatedEntry);
        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion

    #region Delete

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> DeleteAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var existingEntry = await ctx.EducationInstitutions
            .Where(x => x.CommonIdentity == id)
            .ToListAsync();

        if (existingEntry.IsNullOrEmpty()) return Error.New($"404 - No entries with id {id}");
            
        existingEntry = existingEntry.Where(x => !x.IsDeleted).ToList();

        if (existingEntry.IsNullOrEmpty()) return Error.New("204 - Nothing to delete");

        foreach (var entry in existingEntry) entry.IsDeleted = true;

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    public async Task<Fin<EducationInstitutionModel>> DeleteEntryAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new EducationInstitution model"
                    ));
        }

        var existingEntry = await ctx.EducationInstitutions
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