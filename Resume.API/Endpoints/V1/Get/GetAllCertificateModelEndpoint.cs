using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Serilog;
using Gay.TCazier.Resume.API.Mappings.V1;
using Asp.Versioning;
using BeaniesUtilities.APIUtilities.Endpoints;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
//using Gay.TCazier.Resume.API.Auth;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Get;

/// <summary>
/// The collection of endpoints for the Certificate Model in API
/// </summary>
[ApiVersion(1.0)]
public class GetAllCertificateModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Certificates";
    private const string BaseRoute = "Certificates";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";
    /// <summary>
    /// Add the Certificate Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Certificate Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        Log.Information("Now adding Certificate Model get endpoints");
        var singleEndpoint = app.MapGet(EndpointPrefix, GetAllCertificateModelsAsync)
            .WithName("GetAllCertificateModels")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .CacheOutput(Tag)
            .WithTags(Tag);
            
        //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        //{
        //    singleEndpoint.AllowAnonymous();
        //}
        //else
        //{
        //    singleEndpoint.RequireAuthorization(AuthConstants.TrustedMemberPolicyName);
        //}
    }

    /// <summary>
    /// Retrieve all Certificate Models from the database
    /// </summary>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="searchParams"></param>
    /// <param name="token">Cancelation token</param>
    /// <returns>A list of all Certificate Models in the database</returns>
    /// <response code="200">Get all successful</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> GetAllCertificateModelsAsync(ICertificateModelService service,
        [AsParameters] GetAllCertificateModelsRequest searchParams,
        HttpContext content,
        CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        //var userId = content.GetUserId();
        var userId = 0;

        Log.Information("Get All Certificate Models endpoint called by {username}", @username);

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
                var responsesCollection = new CertificateModelsResponse()
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
                Log.Error(Fail.ToException(), "Server issue encountered while trying to get all Certificate Models from the database");
                return Results.Problem(detail: Fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }
}
