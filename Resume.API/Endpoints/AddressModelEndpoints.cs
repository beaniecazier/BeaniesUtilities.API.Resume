using BeaniesUtilities.Models.Resume;
using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Serilog;
using Gay.TCazier.Resume.BLL.Contexts;
using LanguageExt.Common;
using Gay.TCazier.Resume.BLL.Repositories;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Gay.TCazier.Resume.BLL.Services;
using Gay.TCazier.Resume.Contracts.Requests;
using Gay.TCazier.Resume.API.Mappings;
using static LanguageExt.Fin;
using static System.Net.WebRequestMethods;
using System.Reflection;
using LanguageExt;

namespace Gay.TCazier.Resume.API.Endpoints;

/// <summary>
/// The collection of endpoints for the Address Model in API
/// </summary>
public class AddressEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Addresses";
    private const string BaseRoute = "Addresses";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";

    /// <summary>
    /// Add the Address Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        Log.Information("Adding Address Model Service to the DI Container");
        services.AddSingleton<IRepository<AddressModel>, AddressModelInMemRepository>();
        services.AddSingleton<IAddressService, AddressService>();
    }

    /// <summary>
    /// Map all Address Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        Log.Information("Now adding Address Model post endpoints");
        app.MapPost(EndpointPrefix, CreateAddressModelAsync)
            .WithName("CreateAddress")
            .Accepts<AddressModel>(ContentType)
            .Produces<AddressModel>(StatusCodes.Status201Created)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags(Tag);

        // Read Endpoints
        Log.Information("Now adding Address Model get endpoints");
        app.MapGet(EndpointPrefix, GetAllAddressModelsAsync)
            .WithName("GetAllAddressModels")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags(Tag);

        app.MapGet($"{EndpointPrefix}/{{id}}", GetAddressModelByIDAsync)
            .WithName("GetAddressModelByID")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to update
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags(Tag);

        // Update Endpoints
        Log.Information("Now adding Address Model put endpoints");
        app.MapPut($"{EndpointPrefix}/{{id}}", UpdateAddressModelAsync)
            .WithName("UpdateAddressModel")
            .Accepts<AddressModel>(ContentType)
            .Produces<AddressModel>(StatusCodes.Status200OK)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)                                  // you gave bad info
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to update
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags(Tag);

        app.MapPut(EndpointPrefix, PutModelCollectionAsync)
            .Produces(StatusCodes.Status405MethodNotAllowed)
            .WithTags(Tag);

        // Delete Endpoints
        Log.Information("Now adding Address Model delete endpoints");
        app.MapDelete($"{EndpointPrefix}/{{id}}", DeleteAsync)
            .WithName("DeleteAddressModel")
            .Produces(StatusCodes.Status200OK)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)      // you gave bad info
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to delete
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags(Tag);

        app.MapDelete(EndpointPrefix, DeleteAllModelCollection)
            .Produces(StatusCodes.Status405MethodNotAllowed)
            .WithTags(Tag);
    }

    #region Create

    /// <summary>
    /// Create a new Address Model
    /// </summary>
    /// <param name="request">The parameters used to make the new Address Model</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="ctx">The database context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The newly created newModel</returns>
    /// <response code="201">Model was successfully created and added to the database</response>
    /// <response code="400">Invalid information was provided and the request failed validation</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> CreateAddressModelAsync(CreateAddressModelRequest request,
        IAddressService service, LinkGenerator linker, HttpContext http, ResumeContext ctx, CancellationToken token)
    {
        Log.Information("Create Address Model endpoint called");

        int id = await service.GetNextAvailableId();
        var model = request.MapToModelFromCreateRequest(id, "Tiabeanie");
        var validationResult = await service.ValidateModelForCreation(model);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        //var created = await service.CreateAsync(model, token);
        return (await service.CreateAsync(model, token)).Match(
            succ =>
            {
                Log.Information("Address Model Created with id {model.CommonIdentity}", @model.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetAddressModelByID", new { id = model.CommonIdentity });
                return Results.Created(locationUri, succ);
            },
            fail =>
            {
                Log.Error(fail.ToException(), "Server issue encountered while trying to add a new Address Model to the database");
                return Results.Problem(detail: fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            }
        );
    }

    #endregion

    #region Read

    /// <summary>
    /// Retrieve all Address Models from the database
    /// </summary>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="token">Cancelation token</param>
    /// <param name="nameSearchTerm">REGEX used to query for models with matching name field</param>
    /// <param name="notesSearchTerm">REGEX to query for models across all notes</param>
    /// <param name="start">Date and time for start range of query. If left empty, all newModel that were last modified or created before end will be returned</param>
    /// <param name="end">Date and time for the end of the search range. If left empty, all newModel that were last modified or created after start will be returned</param>
    /// <param name="allowHidden">Allow query to include hidden entries</param>
    /// <param name="allowDeleted">Allow query to include deleted entries</param>
    /// <param name="idLowerBound">Id search range lower bound. If left empty, all newModel that with an id before end will be returned</param>
    /// <param name="idUpperBound">Id search range upper bound. If left empty, all newModel that with an id before end will be returned</param>
    /// <returns>A list of all Address Models in the database</returns>
    /// <response code="200">Get all successful</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> GetAllAddressModelsAsync(IAddressService service, CancellationToken token,
        string? nameSearchTerm, string? notesSearchTerm, DateTime? start, DateTime? end, bool? allowHidden, bool? allowDeleted,
        int? idLowerBound, int? idUpperBound)
    {
        Log.Information("Get All Address Models endpoint called");

        var entries = await service.GetAllAsync(token);
        return entries.Match(
            Succ => Results.Ok(Succ.Select(x => x.MapToResponseFromModel())),
            Fail =>
            {
                Log.Error(Fail.ToException(), "Server issue encountered while trying to get all Address Models from the database");
                return Results.Problem(detail: Fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }

    /// <summary>
    /// Query the database by id for the most up to date copy of a newModel
    /// </summary>
    /// <param name="id">The newModel id used to query the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The searched newModel</returns>
    /// <response code="200">Get successful</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> GetAddressModelByIDAsync(int id, IAddressService service, CancellationToken token)
    {
        Log.Information("Get Address Model endpoint called with id");

        var entry = await service.GetByIDAsync(id, token);
        return entry.Match(
            Succ => Results.Ok(Succ!.MapToResponseFromModel()),
            Fail =>
            {
                var ex = Fail.ToException();
                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Log.Error(ex, "Address Model with ID:{id} does not exist", @id);
                    return Results.NotFound();
                }
                Log.Error(ex, $"Server issue encountered while trying to get Address Model with ID:{id} from the database");
                return Results.Problem(detail: ex.ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }

    #endregion

    #region Update

    /// <summary>
    /// Search for and remove a specific Address Model result from the database
    /// </summary>
    /// <param name="changes">The collection of changes to be applied to the newModel</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The updated copy of the Address Model</returns>
    /// <response code="200">Update successful</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> UpdateAddressModelAsync(UpdateAddressModelRequest changes,
        IAddressService service, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        Log.Information("Update Address Model endpoint called");

        var oldModel = await service.GetByIDAsync(changes.Id, token);
        if (oldModel.IsFail)
        {
            Log.Error(((Error)oldModel).ToException(), "Server issue encountered while trying to get all Address Models from the database");
            return Results.Problem(detail: ((Error)oldModel).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        if (oldModel.Head().IsNull())
        {
            Log.Error("Address Model with ID:{id} does not exist", @changes.Id);
            return Results.NotFound();
        }

        var newModel = changes.MapToModelFromUpdateRequest((AddressModel)oldModel!, "Tiabeanie");

        var validationResult = await service.ValidateModelForUpdate(newModel);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        var result = await service.UpdateAsync(newModel, (AddressModel)oldModel, token);
        return result.Match(
            Succ =>
            {
                Log.Information("Address Model with id {model.CommonIdentity} was successfully updated", @newModel.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetAddressModelByID", new { id = newModel.CommonIdentity });
                return Results.Created(locationUri, Succ);
            },
            Fail => Results.Problem(detail: ((Error)oldModel).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    /// <summary>
    /// Not allowed collction put endpoint
    /// </summary>
    /// <returns>405 Method not allowed</returns>
    /// <response code="405">I dont know how you got here, but hun, F*** off, this aint allowed</response>
    private static async Task<IResult> PutModelCollectionAsync() => Results.StatusCode(StatusCodes.Status405MethodNotAllowed);

    #endregion

    #region Delete

    /// <summary>
    /// Delete an Address Model by its id along with all of its history
    /// </summary>
    /// <param name="id">The id of the Address Model to delete form the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>Returns no content on a successful delete</returns>
    /// <response code="200">Delete worked, returns the last surviving copy</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> DeleteAsync(int id, IAddressService service, CancellationToken token)
    {
        Log.Information("Delete Address Model endpoint called");

        var entry = await service.DeleteAsync(id, token);
        return entry.Match(
            Succ =>
            {
                Log.Information("Address Model with id:{Succ.CommonIdentity}", @Succ.CommonIdentity);
                return Results.Ok(Succ.MapToResponseFromModel());
            },
            Fail =>
            {
                var ex = Fail.ToException();

                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Log.Error(ex, "Address Model with ID:{id} does not exist", @id);
                    return Results.NotFound();
                }
                Log.Error(ex, $"Server issue encountered while trying to delete Address Model with ID:{id} from the database");
                return Results.Problem(detail: ex.ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }

    /// <summary>
    /// Not allowed collection delete endpoint
    /// </summary>
    /// <returns>405 Method not allowed</returns>
    /// <response code="405">I dont know how you got here, but hun, F*** off, this aint allowed</response>
    private static async Task<IResult> DeleteAllModelCollection() => Results.StatusCode(StatusCodes.Status405MethodNotAllowed);

    #endregion
}