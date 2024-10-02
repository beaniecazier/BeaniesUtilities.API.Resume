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

public class CertificateService : BaseModelService, ICertificateService
{
    #region Fields

    IServiceProvider _provider;

    #endregion

    #region Constructors

    public CertificateService(IServiceProvider provider)
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
    public async Task<Fin<CertificateModel>> CreateAsync(ResumeContext ctx, EditibleCertificateModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var entries = await ctx.Certificates
            .ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness
        int id = GetNextCommonID(entries);
        var model = new CertificateModel();
        model.Create(id, editibleAttributes, "Tiabeanie");

        await ctx.Certificates.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    #endregion

    #region Read

    public async Task<Fin<IEnumerable<CertificateModel>>> GetAllAsync(ResumeContext ctx)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var entries = await ctx.Certificates
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var entries = await ctx.Certificates
            .Where(x => x.EntryIdentity >= start &&
                        x.EntryIdentity <= end)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var entries = await ctx.Certificates
            .Where(x => x.CommonIdentity >= start &&
                        x.CommonIdentity <= end &&
                        !x.IsHidden)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<CertificateModel>> GetByEntryIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        try
        {
            var entry = await ctx.Certificates
                .Where(x => x.EntryIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    public async Task<Fin<CertificateModel>> GetByIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        try
        {
            var entry = await ctx.Certificates
                .Where(x => x.CommonIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    //public async Task<Fin<IEnumerable<CertificateModel>>> GetHistroyOfIDAsync(ResumeContext ctx, int id)
    //{
    //    if (ctx == null)
    //    {
    //        return Error.New(
    //            new NullReferenceException(
    //                "The provider returned a null DbContext while trying to create a new Certificate model"
    //                ));
    //    }

    //    var entries = await ctx.Certificates
    //        .Where(x => x.EntryIdentity == id)
    //        .ToListAsync();
    //    return entries;
    //}

    #endregion

    #region Query

    public async Task<Fin<IEnumerable<CertificateModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<CertificateModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        throw new NotImplementedException();
    }

    #endregion

    #region Update

    public async Task<Fin<CertificateModel>> UpdateAsync(ResumeContext ctx, int id, EditibleCertificateModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var existingEntry = await ctx.Certificates
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

        await ctx.Certificates.AddAsync(updatedEntry);
        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion

    #region Delete

    public async Task<Fin<IEnumerable<CertificateModel>>> DeleteAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var existingEntry = await ctx.Certificates
            .Where(x => x.CommonIdentity == id)
            .ToListAsync();

        if (existingEntry.IsNullOrEmpty()) return Error.New($"404 - No entries with id {id}");
            
        existingEntry = existingEntry.Where(x => !x.IsDeleted).ToList();

        if (existingEntry.IsNullOrEmpty()) return Error.New("204 - Nothing to delete");

        foreach (var entry in existingEntry) entry.IsDeleted = true;

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    public async Task<Fin<CertificateModel>> DeleteEntryAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Certificate model"
                    ));
        }

        var existingEntry = await ctx.Certificates
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