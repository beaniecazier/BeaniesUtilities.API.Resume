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

/// <summary>
/// Service class for Address Model to handle CRUD operations with database
/// </summary>
public class AddressService : BaseModelService, IAddressService
{
    #region Fields


    #endregion

    #region Constructors


    #endregion

    #region Create

    /// <summary>
    /// Create and insert model into database
    /// </summary>
    /// <param name="ctx">The database context</param>
    /// <param name="editibleAttributes">New model parameters</param>
    /// <returns>Created model or the conditions that caused failure</returns>
    public async Task<Fin<AddressModel>> CreateAsync(ResumeContext ctx, EditibleAddressModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var entries = await ctx.Addresses
            .ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness
        int id = GetNextCommonID(entries);
        var model = new AddressModel();
        model.Create(id, editibleAttributes, "Tiabeanie");

        await ctx.Addresses.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    #endregion

    #region Read

    /// <summary>
    /// Retrieve all models from database
    /// </summary>
    /// <param name="ctx">The database context</param>
    /// <returns>A list of all Address Models or the fail conditions</returns>
    public async Task<Fin<IEnumerable<AddressModel>>> GetAllAsync(ResumeContext ctx)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var entries = await ctx.Addresses
            .ToListAsync();
        return entries;
    }

    /// <summary>
    /// Retrieve specific model by id from database
    /// </summary>
    /// <param name="ctx">The database context</param>
    /// <param name="id">Id of model to retrieve</param>
    /// <returns>Found model or the fail condition</returns>
    public async Task<Fin<AddressModel>> GetByIDAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        try
        {
            var entry = await ctx.Addresses
                .Where(x => x.CommonIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }
    //}

    #endregion

    #region Update

    /// <summary>
    /// Update model of id in the database
    /// </summary>
    /// <param name="ctx">The database context</param>
    /// <param name="id">Id of model to update</param>
    /// <param name="editibleAttributes">Model's new parameters</param>
    /// <returns>The updated model or the fail condition</returns>
    public async Task<Fin<AddressModel>> UpdateAsync(ResumeContext ctx, int id, EditibleAddressModel editibleAttributes)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var existingEntry = await ctx.Addresses
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

        await ctx.Addresses.AddAsync(updatedEntry);
        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete model from database
    /// </summary>
    /// <param name="ctx">The database context</param>
    /// <param name="id">Id of model to delete</param>
    /// <returns>The last copy of the model of the fail condition</returns>
    public async Task<Fin<AddressModel>> DeleteAsync(ResumeContext ctx, int id)
    {
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var existingEntry = await ctx.Addresses
            .Where(x => x.CommonIdentity == id)
            .ToListAsync();

        if (existingEntry.IsNullOrEmpty()) return Error.New($"404 - No entries with id {id}");
            
        existingEntry = existingEntry.Where(x => !x.IsDeleted).ToList();

        if (existingEntry.IsNullOrEmpty()) return Error.New("204 - Nothing to delete");

        foreach (var entry in existingEntry) entry.IsDeleted = true;

        await ctx.SaveChangesAsync();
        var lastEntryId = existingEntry.Max(x => x.EntryIdentity);
        return existingEntry.Find(x => x.EntryIdentity == lastEntryId);
    }

    #endregion
}