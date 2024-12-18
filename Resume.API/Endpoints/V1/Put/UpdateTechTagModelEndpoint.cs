using Serilog;
using FluentValidation.Results;
using LanguageExt.Common;
using LanguageExt;
using Asp.Versioning;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.APIUtilities.Endpoints;
using Gay.TCazier.Resume.API.Mappings.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Put;

/// <summary>
/// The collection of endpoints for the TechTag Model in API
/// </summary>
[ApiVersion(1.0)]
public class UpdateTechTagModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "TechTags";
    private const string BaseRoute = "TechTags";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";

    /// <summary>
    /// Add the TechTag Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all TechTag Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {

        // Update Endpoints
        Log.Information("Now adding TechTag Model put endpoints");
        var singleEndpoint = app.MapPut($"{EndpointPrefix}/{{id}}", UpdateTechTagModelAsync)
            .WithName("UpdateTechTagModel")
            .Accepts<TechTagModel>(ContentType)
            .Produces<TechTagModel>(StatusCodes.Status200OK)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)                                  // you gave bad info
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to update
            .Produces(StatusCodes.Status500InternalServerError)
            //.RequireAuthorization(AuthConstants.TrustedMemberPolicyName)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);

        var multipleEndpoint = app.MapPut(EndpointPrefix, PutModelCollectionAsync)
            .Produces(StatusCodes.Status405MethodNotAllowed)
            //.RequireAuthorization(AuthConstants.AdminUserPolicyName)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);
    }

    /// <summary>
    /// Search for and remove a specific TechTag Model result from the database
    /// </summary>
    /// <param name="changes">The collection of changes to be applied to the newModel</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The updated copy of the TechTag Model</returns>
    /// <response code="200">Update successful</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> UpdateTechTagModelAsync(UpdateTechTagModelRequest changes,
        ITechTagModelService service, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Update TechTag Model endpoint called by {username}", @username);

        var oldModel = await service.GetByIDAsync(changes.Id, token);
        if (oldModel.IsFail)
        {
            Log.Error(((Error)oldModel).ToException(), "Server issue encountered while trying to get all TechTag Models from the database");
            return Results.Problem(detail: ((Error)oldModel).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        if (oldModel.Head().IsNull())
        {
            Log.Error("TechTag Model with ID:{id} does not exist", @changes.Id);
            return Results.NotFound();
        }
        

        var newModel = changes.MapToModelFromUpdateRequest((TechTagModel)oldModel!, username);

        var validationResult = await service.ValidateModelForUpdate(newModel);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        var result = await service.UpdateAsync(newModel, (TechTagModel)oldModel, token);
        return result.Match(
            Succ =>
            {
                Log.Information("TechTag Model with id {model.CommonIdentity} was successfully updated", @newModel.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetTechTagModelByID", new { id = newModel.CommonIdentity });
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

}