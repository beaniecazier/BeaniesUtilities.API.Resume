using BeaniesUtilities.Models.Resume;
using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Traits;

namespace Gay.TCazier.DatabaseParser.Endpoints;

/// <summary>
/// The collection of endpoints for the Address Model in API
/// </summary>
public class AddressEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Address Model";

    /// <summary>
    /// Address Model API base route
    /// </summary>
    public const string BaseRoute = "Addresses";

    /// <summary>
    /// Add the Address Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAddressService, AddressService>();
    }

    /// <summary>
    /// Map all Address Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
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
    /// Create a new Address Model
    /// </summary>
    /// <param name="newEntry">The parameters used to make the new Address Model</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="vaildator">Address Model value validator</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="ctx">The database context</param>
    /// <returns>The newly created model</returns>
    /// <response code="201">Model was successfully created and added to the database</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    private static async Task<IResult> CreateAddressModelAsync(EditibleAddressModel newEntry,
        IAddressService service, IValidator<EditibleAddressModel> vaildator,
        LinkGenerator linker, HttpContext http, ResumeContext ctx)
    {
        var validationResult = await vaildator.ValidateAsync(newEntry);

        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var created = await service.CreateAsync(ctx, newEntry);

        //return Results.BadRequest(new List<ValidationFailure>
        //{
        //    new ("ID", "This Address already exists in the database." ),
        //});
        return created.Match(
            succ =>
            {
                var locationUri = linker.GetUriByName(http, "GetAddressModel", new { id = succ.CommonIdentity });
                return Results.Created(locationUri, succ.CommonIdentity);
            },
            fail => Results.BadRequest(fail.ToException()));
    }

    #endregion

    #region Read

    /// <summary>
    /// Retrieve all Address Models from the database
    /// </summary>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">The database context</param>
    /// <param name="nameSearchTerm">REGEX used to query for models with matching name field</param>
    /// <param name="notesSearchTerm">REGEX to query for models across all notes</param>
    /// <param name="start">Date and time for start range of query. If left empty, all model that were last modified or created before end will be returned</param>
    /// <param name="end">Date and time for the end of the search range. If left empty, all model that were last modified or created after start will be returned</param>
    /// <param name="allowHidden">Allow query to include hidden entries</param>
    /// <param name="allowDeleted">Allow query to include deleted entries</param>
    /// <param name="idLowerBound">Id search range lower bound. If left empty, all model that with an id before end will be returned</param>
    /// <param name="idUpperBound">Id search range upper bound. If left empty, all model that with an id before end will be returned</param>
    /// <returns>A list of all Address Models in the database</returns>
    /// <response code="200">Get all successful</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    private static async Task<IResult> GetAllAddressModelsAsync(IAddressService service, ResumeContext ctx, string? nameSearchTerm,
        string? notesSearchTerm, DateTime? start, DateTime? end, bool? allowHidden, bool? allowDeleted,
        int? idLowerBound, int? idUpperBound)
    {
        var entries = await service.GetAllAsync(ctx);
        return entries.Match(
            Succ => Results.Ok(entries),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    /// <summary>
    /// Query the database by id for the most up to date copy of a model
    /// </summary>
    /// <param name="id">The model id used to query the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">The database context</param>
    /// <returns>The searched model</returns>
    /// <response code="200">Get successful</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">Id was not found in the database</response>
    private static async Task<IResult> GetAddressModelByIDAsync(int id, IAddressService service, ResumeContext ctx)
    {
        var entry = await service.GetByIDAsync(ctx, id);
        return entry.Match(
            Succ => Succ is null ? Results.NotFound() : Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    #endregion

    #region Update

    /// <summary>
    /// Search for and remove a specific Address Model entry from the database
    /// </summary>
    /// <param name="id">The id of the model to update</param>
    /// <param name="changes">The collection of changes to be applied to the model</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="vaildator">Address Model value validator</param>
    /// <param name="ctx">The database context</param>
    /// <returns>The updated copy of the Address Model</returns>
    /// <response code="200">Update successful</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">Id was not found in the database</response>
    private static async Task<IResult> UpdateAddressModelAsync(int id, EditibleAddressModel changes,
        IAddressService service, IValidator<EditibleAddressModel> vaildator, ResumeContext ctx)
    {
        var validationResult = await vaildator.ValidateAsync(changes);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var entry = await service.UpdateAsync(ctx, id, changes);
        return entry.Match(
            Succ => Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete an Address Model by its id along with all of its history
    /// </summary>
    /// <param name="id">The id of the Address Model to delete form the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">The database context</param>
    /// <returns>Returns no content on a successful delete</returns>
    /// <response code="200">Delete worked, returns the last surviving copy</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">Id was not found in the database</response>
    private static async Task<IResult> DeleteAsync(int id, IAddressService service, ResumeContext ctx)
    {
        var entry = await service.DeleteAsync(ctx, id);
        return entry.Match(
            Succ => Results.Ok(Succ),
            Fail =>
            {
                var ex = Fail.ToException();
                if (ex.Message == "204 - Nothing to delete") return Results.NotFound();
                if (ex.Message == $"404 - No entries with id {id}") return Results.NotFound();
                return Results.BadRequest(ex);
            });
    }

    #endregion
}