using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.APIUtilities.Endpoints;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
using LanguageExt;
using LanguageExt.Common;
using Asp.Versioning;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Gay.TCazier.Resume.API.Mappings.V1;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Create;

/// <summary>
/// The collection of endpoints for the Person Model in API
/// </summary>
[ApiVersion(1.0)]
public class CreatePersonModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "People";
    private const string BaseRoute = "People";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";

    /// <summary>
    /// Add the Person Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Person Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        Log.Information("Now adding Person Model post endpoints");
        var singleEndpoint = app.MapPost(EndpointPrefix, CreatePersonModelAsync)
            .WithName("CreatePerson")
            .Accepts<PersonModel>(ContentType)
            .Produces<PersonModel>(StatusCodes.Status201Created)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            //.RequireAuthorization(AuthConstants.TrustedMemberPolicyName)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);
    }

    #region Create

    /// <summary>
    /// Create a new Person Model
    /// </summary>
    /// <param name="request">The parameters used to make the new Person Model</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="ctx">The database context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The newly created newModel</returns>
    /// <response code="201">Model was successfully created and added to the database</response>
    /// <response code="400">Invalid information was provided and the request failed validation</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> CreatePersonModelAsync(CreatePersonModelRequest request,
        IPersonModelService service, IAddressModelService addressService, IPhoneNumberModelService phoneNumberService, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Create Person Model endpoint called by {username}", @username);

        int id = await service.GetNextAvailableId();
        
        var requestedAddressModels = await addressService.GetAllAsync(new GetAllAddressModelsOptions {SpecificIds = request.Addresses}, token);
        if (requestedAddressModels.IsFail)
        {
            Log.Error(((Error)requestedAddressModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedAddressModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedPhoneNumberModels = await phoneNumberService.GetAllAsync(new GetAllPhoneNumberModelsOptions {SpecificIds = request.PhoneNumbers}, token);
        if (requestedPhoneNumberModels.IsFail)
        {
            Log.Error(((Error)requestedPhoneNumberModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedPhoneNumberModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var model = request.MapToModelFromCreateRequest(id, username, (List<AddressModel>)requestedAddressModels, (List<PhoneNumberModel>)requestedPhoneNumberModels);
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
                Log.Information("Person Model Created with id {model.CommonIdentity}", @model.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetPersonModelByID", new { id = model.CommonIdentity });
                return Results.Created(locationUri, succ);
            },
            fail =>
            {
                Log.Error(fail.ToException(), "Server issue encountered while trying to add a new Person Model to the database");
                return Results.Problem(detail: fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            }
        );
    }

    #endregion
}