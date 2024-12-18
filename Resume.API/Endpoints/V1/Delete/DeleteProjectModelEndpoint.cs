using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Serilog;
using Gay.TCazier.Resume.API.Mappings.V1;
using Asp.Versioning;
using BeaniesUtilities.APIUtilities.Endpoints;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Delete;

/// <summary>
/// The collection of endpoints for the Project Model in API
/// </summary>
[ApiVersion(1.0)]
public class DeleteProjectModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Projects";
    private const string BaseRoute = "Projects";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";

    /// <summary>
    /// Add the Project Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Project Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Delete Endpoints
        Log.Information("Now adding Project Model delete endpoints");
        var singleEndpoint = app.MapDelete($"{EndpointPrefix}/{{id}}", DeleteAsync)
            .WithName("DeleteProjectModel")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to delete
            .Produces(StatusCodes.Status500InternalServerError)
            //.RequireAuthorization(AuthConstants.AdminUserPolicyName)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);

        var multipleEndpoint = app.MapDelete(EndpointPrefix, DeleteAllModelCollection)
            .Produces(StatusCodes.Status405MethodNotAllowed)
            //.RequireAuthorization(AuthConstants.AdminUserPolicyName)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);
    }

    /// <summary>
    /// Delete an Project Model by its id along with all of its history
    /// </summary>
    /// <param name="id">The id of the Project Model to delete form the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>Returns no content on a successful delete</returns>
    /// <response code="200">Delete worked, returns the last surviving copy</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> DeleteAsync(int id, IProjectModelService service, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Delete Project Model endpoint called by {username}", @username);

        var entry = await service.DeleteAsync(id, token);
        return entry.Match(
            Succ =>
            {
                Log.Information("Project Model with id:{Succ.CommonIdentity}", @Succ.CommonIdentity);
                return Results.Ok(Succ.MapToResponseFromModel());
            },
            Fail =>
            {
                var ex = Fail.ToException();

                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Log.Error(ex, "Project Model with ID:{id} does not exist", @id);
                    return Results.NotFound();
                }
                Log.Error(ex, $"Server issue encountered while trying to delete Project Model with ID:{id} from the database");
                return Results.Problem(detail: ex.ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }

    /// <summary>
    /// Not allowed collection delete endpoint
    /// </summary>
    /// <returns>405 Method not allowed</returns>
    /// <response code="405">I dont know how you got here, but hun, F*** off, this aint allowed</response>
    private static async Task<IResult> DeleteAllModelCollection() => Results.StatusCode(StatusCodes.Status405MethodNotAllowed);
}
