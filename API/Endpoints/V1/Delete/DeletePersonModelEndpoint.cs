using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
//using Gay.TCazier.Resume.API.Auth;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Gay.TCazier.Resume.API.Mappings.V1;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;
using BeaniesUtilities.APIUtilities.Endpoints;
using Serilog;
using Asp.Versioning;
using Microsoft.AspNetCore.OutputCaching;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Delete;

/// <summary>
/// The collection of Endpoints for the Person Model in API
/// </summary>
[ApiVersion(1.0)]
public class DeletePersonModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";

    /// <summary>
    /// Add the Person Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Person Model Endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Delete Endpoints
        Log.Information("Now adding Person Model Delete Endpoints");
        var singleEndpoint = app.MapDelete(PersonModelEndpoints.Delete, DeleteAsync)
            .WithName("DeletePersonModel")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to delete
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(PersonModelEndpoints.Tag);

        //var multipleEndpoint = app.MapDelete(PersonModelEndpoints.EndpointPrefix, DeleteAllModelCollection)
        //    .Produces(StatusCodes.Status405MethodNotAllowed)
        //    .WithApiVersionSet(APIVersioning.VersionSet)
        //    .HasApiVersion(1.0)
        //    .WithTags(PersonModelEndpoints.Tag);
            
        //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        //{
        //    singleEndpoint.AllowAnonymous();
        //    multipleEndpoint.AllowAnonymous();
        //}
        //else
        //{
        //    singleEndpoint.RequireAuthorization(AuthConstants.AdminUserPolicyName);
        //    multipleEndpoint.RequireAuthorization(AuthConstants.AdminUserPolicyName);
        //}
    }

    /// <summary>
    /// Delete an Person Model by its id along with all of its history
    /// </summary>
    /// <param name="id">The id of the Person Model to delete form the database</param>
    /// <param name="service">The service class the serves this Endpoint for database operations</param>
    /// <param name="outputCacheStore">Access to the Output Cache</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>Returns no content on a successful delete</returns>
    /// <response code="200">Delete worked, returns the last surviving copy</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> DeleteAsync(int id, IPersonModelService service,
        IOutputCacheStore outputCacheStore, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Delete Person Model Endpoint called by {username}", @username);

        var entry = await service.DeleteAsync(id, token);
        if (!entry.IsFail) await outputCacheStore.EvictByTagAsync(PersonModelEndpoints.Tag, token);
        return entry.Match(
            Succ =>
            {
                Log.Information("Person Model with id:{Succ.CommonIdentity}", @Succ.CommonIdentity);
                return Results.Ok(Succ.MapToResponseFromModel());
            },
            Fail =>
            {
                var ex = Fail.ToException();

                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Log.Error(ex, "Person Model with ID:{id} does not exist", @id);
                    return Results.NotFound();
                }
                Log.Error(ex, $"Server issue encountered while trying to delete Person Model with ID:{id} from the database");
                return Results.Problem(detail: ex.ToString(), statusCode: StatusCodes.Status500InternalServerError);
            });
    }

    /// <summary>
    /// Not allowed Collection Delete Endpoint
    /// </summary>
    /// <returns>405 Method not allowed</returns>
    /// <response code="405">I dont know how you got here, but hun, F*** off, this aint allowed</response>
    private static async Task<IResult> DeleteAllModelCollection() => Results.StatusCode(StatusCodes.Status405MethodNotAllowed);
}
