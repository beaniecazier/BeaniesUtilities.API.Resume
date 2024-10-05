using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

#pragma warning disable CS1591

public interface IAddressService
{
    public Task<Fin<IEnumerable<AddressModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<AddressModel>> GetByIDAsync(ResumeContext ctx, int id);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<AddressModel>> CreateAsync(ResumeContext ctx, EditibleAddressModel editibleAttributes);

    public Task<Fin<AddressModel>> UpdateAsync(ResumeContext ctx, int id, EditibleAddressModel editibleAttributes);

    public Task<Fin<AddressModel>> DeleteAsync(ResumeContext ctx, int id);
}

#pragma warning restore CS1591