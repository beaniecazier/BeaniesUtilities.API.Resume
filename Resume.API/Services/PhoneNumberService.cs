using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Services;

public class PhoneNumberService : BaseModelService, IPhoneNumberService
{

    IServiceProvider _provider;

    public PhoneNumberService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Fin<PhoneNumberModel>> CreateAsync(EditiblePhoneNumberModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException("The provider returned a null DbContext while trying to create a new Address model"));
        }

        var entries = await ctx.PhoneNumbers.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new PhoneNumberModel()
        {
            CountryCode = editibleAttributes.CountryCode,
            AreaCode = editibleAttributes.AreaCode,
            TelephonePrefix = editibleAttributes.TelephonePrefix,
            LineNumber = editibleAttributes.LineNumber,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.PhoneNumbers.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    public async Task<Fin<PhoneNumberModel>> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        var entries = await ctx.PhoneNumbers.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<PhoneNumberModel>> GetByEntryIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<PhoneNumberModel>> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<PhoneNumberModel>> GetHistroyOfIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByAreaCodeAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByCountryCodeAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByNotesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<PhoneNumberModel>> UpdateAsync(EditiblePhoneNumberModel editibleAttributes)
    {
        throw new NotImplementedException();
    }
}
