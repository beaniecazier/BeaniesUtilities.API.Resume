using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Services;

public class EducationInstituteService : BaseModelService, IEducationInstituteService
{

    IServiceProvider _provider;

    public EducationInstituteService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Fin<EducationInstitutionModel>> CreateAsync(EditibleEduInstituteModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException("The provider returned a null DbContext while trying to create a new Address model"));
        }

        var entries = await ctx.Institutions.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new EducationInstitutionModel()
        {
            Website = editibleAttributes.Website,
            Address = editibleAttributes.Address,
            CertificatesIssued = editibleAttributes.CertificatesIssued,
            DegreesGiven = editibleAttributes.DegreesGiven,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.Institutions.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    public async Task<Fin<EducationInstitutionModel>> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        var entries = await ctx.Institutions.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationInstitutionModel>> GetByEntryIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationInstitutionModel>> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationInstitutionModel>> GetHistroyOfIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNotesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationInstitutionModel>> UpdateAsync(EditibleEduInstituteModel editibleAttributes)
    {
        throw new NotImplementedException();
    }
}