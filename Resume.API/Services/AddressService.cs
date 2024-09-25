using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Net;
using LanguageExt.Common;
using Gay.TCazier.DatabaseParser.Models.Extensions;

namespace Gay.TCazier.DatabaseParser.Services;

public class AddressService : BaseModelService, IAddressService
{
    #region Fields

    IServiceProvider _provider;

    #endregion

    #region Constructors

    public AddressService(IServiceProvider provider)
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
    public async Task<Fin<AddressModel>> CreateAsync(EditibleAddressModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var entries = await ctx.Addresses.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new AddressModel()
        {
            HouseNumber = editibleAttributes.HouseNumber,
            StreetName = editibleAttributes.StreetName,
            StreetType = editibleAttributes.StreetType,
            City = editibleAttributes.City,
            Region = editibleAttributes.Region,
            State = editibleAttributes.State,
            Country = editibleAttributes.Country,
            PostalCode = editibleAttributes.PostalCode,
            Zip4 = editibleAttributes.Zip4,
            CrossStreetName = editibleAttributes.CrossStreetName,
            PrefixDirection = editibleAttributes.PrefixDirection,
            PrefixType = editibleAttributes.PrefixType,
            SuffixDirection = editibleAttributes.SuffixDirection,
            SuffixType = editibleAttributes.SuffixType,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.Addresses.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    #endregion

    #region Read

    public async Task<Fin<IEnumerable<AddressModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var entries = await ctx.Addresses.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<AddressModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var entries = await ctx.Addresses
            .Where(x => x.EntryIdentity >= start &&
                        x.EntryIdentity <= end)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<AddressModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var entries = await ctx.Addresses
            .Where(x => x.CommonIdentity >= start &&
                        x.CommonIdentity <= end &&
                        !x.IsHidden)
            .ToListAsync();
        return entries;
    }

    public async Task<Fin<AddressModel>> GetByEntryIDAsync(int id)
    {
        var ctx = _provider.GetService<ResumeContext>();
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
                .Where(x => x.EntryIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    public async Task<Fin<AddressModel>> GetByIDAsync(int id)
    {
        var ctx = _provider.GetService<ResumeContext>();
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
                .Where(x => x.EntryIdentity == id)
                .SingleOrDefaultAsync();
            return entry;
        }
        catch (Exception ex)
        {
            return Error.New(ex);
        }
    }

    //public async Task<Fin<IEnumerable<AddressModel>>> GetHistroyOfIDAsync(int id)
    //{
    //    var ctx = _provider.GetService<ResumeContext>();
    //    if (ctx == null)
    //    {
    //        return Error.New(
    //            new NullReferenceException(
    //                "The provider returned a null DbContext while trying to create a new Address model"
    //                ));
    //    }

    //    var entries = await ctx.Addresses
    //        .Where(x => x.EntryIdentity == id)
    //        .ToListAsync();
    //    return entries;
    //}

    #endregion

    #region Query

    public async Task<Fin<IEnumerable<AddressModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByNameAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByNotesAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByCityAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByCountryAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException(
                "The provider returned a null DbContext while trying to create a new Address model"
                ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByStateAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByStreetNameAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<AddressModel>>> SearchByZipAsync(string searchTerm)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        throw new NotImplementedException();
    }

    #endregion

    #region Update

    public async Task<Fin<AddressModel>> UpdateAsync(int id, EditibleAddressModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
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

    public async Task<Fin<AddressModel>> DeleteAsync(int id)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(
                new NullReferenceException(
                    "The provider returned a null DbContext while trying to create a new Address model"
                    ));
        }

        var existingEntry = await ctx.Addresses
            .Where(x => x.CommonIdentity == id &&
                (!x.IsHidden || 
                !x.IsDeleted))
            .SingleOrDefaultAsync();

        if (existingEntry is null) return existingEntry;

        ctx.Addresses.Remove(existingEntry);

        await ctx.SaveChangesAsync();

        return existingEntry;
    }

    #endregion
}
