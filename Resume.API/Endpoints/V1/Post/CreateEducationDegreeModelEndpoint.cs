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
using Gay.TCazier.Resume.Contracts.Requests.V1;
using Gay.TCazier.Resume.BLL.Options.V1;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Create;

/// <summary>
/// The collection of endpoints for the EducationDegree Model in API
/// </summary>
[ApiVersion(1.0)]
public class CreateEducationDegreeModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "EducationDegrees";
    private const string BaseRoute = "EducationDegrees";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";

    /// <summary>
    /// Add the EducationDegree Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all EducationDegree Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        Log.Information("Now adding EducationDegree Model post endpoints");
        var singleEndpoint = app.MapPost(EndpointPrefix, CreateEducationDegreeModelAsync)
            .WithName("CreateEducationDegree")
            .Accepts<EducationDegreeModel>(ContentType)
            .Produces<EducationDegreeModel>(StatusCodes.Status201Created)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            //.RequireAuthorization(AuthConstants.TrustedMemberPolicyName)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);
    }

    #region Create

    /// <summary>
    /// Create a new EducationDegree Model
    /// </summary>
    /// <param name="request">The parameters used to make the new EducationDegree Model</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="ctx">The database context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The newly created newModel</returns>
    /// <response code="201">Model was successfully created and added to the database</response>
    /// <response code="400">Invalid information was provided and the request failed validation</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> CreateEducationDegreeModelAsync(CreateEducationDegreeModelRequest request,
        IEducationDegreeModelService service, IEducationInstitutionModelService educationInstitutionService, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Create EducationDegree Model endpoint called by {username}", @username);

        int id = await service.GetNextAvailableId();
        
        var requestedEducationInstitutionModel = await educationInstitutionService.GetByIDAsync(request.Institution, token);
        if (requestedEducationInstitutionModel.IsFail)
        {
            Log.Error(((Error)requestedEducationInstitutionModel).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedEducationInstitutionModel).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var model = request.MapToModelFromCreateRequest(id, username, (EducationInstitutionModel)requestedEducationInstitutionModel);
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
                Log.Information("EducationDegree Model Created with id {model.CommonIdentity}", @model.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetEducationDegreeModelByID", new { id = model.CommonIdentity });
                return Results.Created(locationUri, succ);
            },
            fail =>
            {
                Log.Error(fail.ToException(), "Server issue encountered while trying to add a new EducationDegree Model to the database");
                return Results.Problem(detail: fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            }
        );
    }

    #endregion
}