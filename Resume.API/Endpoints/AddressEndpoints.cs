using BeaniesUtilities.Models.Resume;
using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Traits;

namespace Gay.TCazier.DatabaseParser.Endpoints;

public class AddressEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Addresses";
    private const string BaseRoute = "addresses";

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAddressService,AddressService>();
    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        app.MapPost(BaseRoute, CreateAddressModelAsync)
            .WithName("CreateAddress")
            .Accepts<EditibleAddressModel>(ContentType)
            .Produces<AddressModel>(201)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        // Read Endpoints
        app.MapGet(BaseRoute, GetAllAddressModelsAsync)
            .WithName("GetAllAddressModels")
            .Produces(200)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/{{id}}", GetAddressModelByIDAsync)
            .WithName("GetAddressModelByID")
            .Produces(200)
            .Produces<IEnumerable<ValidationFailure>>(400)      // you gave bad info
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/entry{{entryId}}", GetAddressModelByEntryIDAsync)
            .WithName("GetAddressModelByEntryID")
            .Produces(200)
            .Produces<IEnumerable<ValidationFailure>>(400)      // you gave bad info
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);

        //app.MapGet($"{BaseRoute}/history{{id}}", GetHistroyOfAddressModelByIDAsync)
        //    .WithName("GetHistroyOfAddressModelByID")
        //    .WithTags(Tag);

        // Update Endpoints
        app.MapPut($"{BaseRoute}/{{id}}", UpdateAddressModelAsync)
            .WithName("UpdateAddressModel")
            .Accepts<EditibleAddressModel>(ContentType)
            .Produces<AddressModel>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)      // you gave bad info
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);

        // Delete Endpoints
        app.MapDelete($"{BaseRoute}/{{id}}", DeleteAsync)
            .WithName("DeleteAddressModel")
            .Produces(200)
            .Produces(204)
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);
    }

    #region Create

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newEntry"></param>
    /// <param name="service"></param>
    /// <param name="vaildator"></param>
    /// <returns></returns>
    private static async Task<IResult> CreateAddressModelAsync(EditibleAddressModel newEntry,
        IAddressService service, IValidator<EditibleAddressModel> vaildator,
        LinkGenerator linker, HttpContext context)
    {
        var validationResult = await vaildator.ValidateAsync(newEntry);

        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var created = await service.CreateAsync(newEntry);

        //return Results.BadRequest(new List<ValidationFailure>
        //{
        //    new ("ID", "This address already exists in the database." ),
        //});
        return created.Match(
            succ =>
            {
                var locationUri = linker.GetUriByName(context, "GetAddressModel", new { id = succ.CommonIdentity });
                return Results.Created(locationUri, succ.CommonIdentity);
            },
            fail => Results.BadRequest(fail.ToException()));
    }

    #endregion

    #region Read

    private static async Task<IResult> GetAllAddressModelsAsync(IAddressService service, string? nameSearchTerm,
        string? notesSearchTerm, DateTime? start, DateTime? end, bool? isHidden, bool? isDeleted,
        int? idLowerBound, int? idUpperBound, int? entryLowerBound, int? entryUpperBound)
    {
        var entries = await service.GetAllAsync();
        return entries.Match(
            Succ => Results.Ok(entries),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    private static async Task<IResult> GetAddressModelByIDAsync(int id, IAddressService service)
    {
        var entry = await service.GetByIDAsync(id);
        return entry.Match(
            Succ => Succ is null ? Results.NotFound() : Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    private static async Task<IResult> GetAddressModelByEntryIDAsync(int entryId,IAddressService service)
    {
        var entry = await service.GetByEntryIDAsync(entryId);
        return entry.Match(
            Succ => Succ is null ? Results.NotFound() : Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    //private static async Task<IResult> GetHistroyOfAddressModelByIDAsync(int id, IAddressService service)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region Update

    private static async Task<IResult> UpdateAddressModelAsync(int id, EditibleAddressModel changes,
        IAddressService service, IValidator<EditibleAddressModel> vaildator)
    {
        var validationResult = await vaildator.ValidateAsync(changes);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var entry = await service.UpdateAsync(id, changes);
        return entry.Match(
            Succ => Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    #endregion

    #region Delete

    private static async Task<IResult> DeleteAsync(int id, IAddressService service)
    {
        var entry = await service.DeleteAsync(id);
        return entry.Match(
            Succ => Results.NoContent(),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    #endregion
}