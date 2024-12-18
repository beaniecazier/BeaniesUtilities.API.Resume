using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Serilog;
using Gay.TCazier.Resume.API.Mappings.V1;
using Asp.Versioning;
using BeaniesUtilities.APIUtilities.Endpoints;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Get;

/// <summary>
/// The collection of endpoints for the EducationDegree Model in API
/// </summary>
[ApiVersion(1.0)]
public class GetEducationDegreeModelEndpoint : IEndpoints
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
        // Read Endpoints
        var singleEndpoint = app.MapGet($"{EndpointPrefix}/{{id}}", GetEducationDegreeModelByIDAsync)
            .WithName("GetEducationDegreeModelByID")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to update
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);
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
    private static async Task<IResult> GetEducationDegreeModelByIDAsync(int id, IEducationDegreeModelService service, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Get EducationDegree Model endpoint called with id by {username}", @username);

        var entry = await service.GetByIDAsync(id, token);
        return entry.Match(
            Succ => Results.Ok(Succ!.MapToResponseFromModel()),
            Fail =>
            {
                var ex = Fail.ToException();
                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Log.Error(ex, "EducationDegree Model with ID:{id} does not exist", @id);
                    return Results.NotFound();
                }
                Log.Error(ex, $"Server issue encountered while trying to get EducationDegree Model with ID:{id} from the database");
                return Results.Problem(detail: ex.ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }
}
