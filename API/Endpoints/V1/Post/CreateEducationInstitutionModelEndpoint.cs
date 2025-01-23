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
//using Gay.TCazier.Resume.API.Auth;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;
using Microsoft.AspNetCore.OutputCaching;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Create;

/// <summary>
/// The collection of Endpoints for the EducationInstitution Model in API
/// </summary>
[ApiVersion(1.0)]
public class CreateEducationInstitutionModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";

    /// <summary>
    /// Add the EducationInstitution Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all EducationInstitution Model Endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        Log.Information("Now adding EducationInstitution Model post Endpoints");
        var singleEndpoint = app.MapPost(EducationInstitutionModelEndpoints.Post, CreateEducationInstitutionModelAsync)
            .WithName("CreateEducationInstitution")
            .Accepts<EducationInstitutionModel>(ContentType)
            .Produces<EducationInstitutionModel>(StatusCodes.Status201Created)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(EducationInstitutionModelEndpoints.Tag);

        //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        //{
        //    singleEndpoint.AllowAnonymous();
        //}
        //else
        //{
        //    singleEndpoint.RequireAuthorization(AuthConstants.AdminUserPolicyName);
        //}
    }

    #region Create

    /// <summary>
    /// Create a new EducationInstitution Model
    /// </summary>
    /// <param name="request">The parameters used to make the new EducationInstitution Model</param>
    /// <param name="service">The service class the serves this Endpoint for database operations</param>
    /// <param name="outputCacheStore">Access to the Output Cache</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="ctx">The database context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The newly created newModel</returns>
    /// <response code="201">Model was successfully created and added to the database</response>
    /// <response code="400">Invalid information was provided and the request failed validation</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> CreateEducationInstitutionModelAsync(CreateEducationInstitutionModelRequest request,
        IEducationInstitutionModelService service, IAddressModelService addressService,
        IOutputCacheStore outputCacheStore, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Create EducationInstitution Model Endpoint called by {username}", @username);

        int id = await service.GetNextAvailableId();
        
        var requestedAddressModel = await addressService.GetByIDAsync(request.Address, token);
        if (requestedAddressModel.IsFail)
        {
            Log.Error(((Error)requestedAddressModel).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedAddressModel).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var model = request.MapToModelFromCreateRequest(id, username, (AddressModel)requestedAddressModel);
        var validationResult = await service.ValidateModelForCreation(model);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        var created = await service.CreateAsync(model, token);
        if(!created.IsFail) await outputCacheStore.EvictByTagAsync(EducationInstitutionModelEndpoints.Tag, token);
        return created.Match(
            succ =>
            {
                Log.Information("EducationInstitution Model Created with id {model.CommonIdentity}", @model.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetEducationInstitutionModelByID", new { id = model.CommonIdentity });
                return Results.Created(locationUri, succ);
            },
            fail =>
            {
                Log.Error(fail.ToException(), "Server issue encountered while trying to add a new EducationInstitution Model to the database");
                return Results.Problem(detail: fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            }
        );
    }

    #endregion
}