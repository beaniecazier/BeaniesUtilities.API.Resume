using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Gay.TCazier.Resume.API.Mappings.V1;
using Serilog;
using Asp.Versioning;
using BeaniesUtilities.APIUtilities.Endpoints;
//using Gay.TCazier.Resume.API.Auth;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Get;

/// <summary>
/// The collection of endpoints for the Person Model in API
/// </summary>
[ApiVersion(1.0)]
public class GetPersonModelEndpoint : IEndpoints
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
        // Read Endpoints
        var singleEndpoint = app.MapGet($"{EndpointPrefix}/{{id}}", GetPersonModelByIDAsync)
            .WithName("GetPersonModelByID")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to update
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .CacheOutput(Tag)
            .WithTags(Tag);

        var multipleEndpoint = app.MapGet(EndpointPrefix, GetAllPersonModelsAsync)
            .WithName("GetAllPersonModels")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .CacheOutput(Tag)
            .WithTags(Tag);

        //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        //{
        //    singleEndpoint.AllowAnonymous();
        //    multipleEndpoint.AllowAnonymous();
        //}
        //else
        //{
        //    singleEndpoint.RequireAuthorization(AuthConstants.TrustedMemberPolicyName);
        //    multipleEndpoint.RequireAuthorization(AuthConstants.AdminUserPolicyName);
        //}
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
    private static async Task<IResult> GetPersonModelByIDAsync(int id, IPersonModelService service, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Get Person Model endpoint called with id by {username}", @username);

        var entry = await service.GetByIDAsync(id, token);
        return entry.Match(
            Succ => Results.Ok(Succ!.MapToResponseFromModel()),
            Fail =>
            {
                var ex = Fail.ToException();
                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Log.Error(ex, "Person Model with ID:{id} does not exist", @id);
                    return Results.NotFound();
                }
                Log.Error(ex, $"Server issue encountered while trying to get Person Model with ID:{id} from the database");
                return Results.Problem(detail: ex.ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }

    /// <summary>
    /// Retrieve all Person Models from the database
    /// </summary>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="searchParams"></param>
    /// <param name="token">Cancelation token</param>
    /// <returns>A list of all Person Models in the database</returns>
    /// <response code="200">Get all successful</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> GetAllPersonModelsAsync(IPersonModelService service,
        [AsParameters] GetAllPersonModelsRequest searchParams,
        HttpContext content,
        CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        //var userId = content.GetUserId();
        var userId = 0;

        Log.Information("Get All Person Models endpoint called by {username}", @username);

        var options = searchParams.MapToOptions().WithID(userId);
        var validationResult = await service.ValidateGetAllModelOptions(options);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        var entries = await service.GetAllAsync(options, token);
        var total = await service.GetQueryTotal(options);
        return entries.Match(
            Succ =>
            {
                var responsesCollection = new PersonModelsResponse()
                {
                    Items = Succ.Select(x => x.MapToResponseFromModel()),
                    PageIndex = searchParams.PageIndex,
                    PageSize = searchParams.PageSize,
                    TotalNumberOfAvailableResponses = total,
                };
                return Results.Ok(responsesCollection);
            },
            Fail =>
            {
                Log.Error(Fail.ToException(), "Server issue encountered while trying to get all Person Models from the database");
                return Results.Problem(detail: Fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }
}
