using Serilog;
using FluentValidation.Results;
using LanguageExt.Common;
using LanguageExt;
using Asp.Versioning;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.APIUtilities.Endpoints;
using Gay.TCazier.Resume.API.Mappings.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
//using Gay.TCazier.Resume.API.Auth;
using Gay.TCazier.Resume.BLL.Services.Interfaces;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Put;

/// <summary>
/// The collection of endpoints for the Resume Model in API
/// </summary>
[ApiVersion(1.0)]
public class UpdateResumeModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Resumes";
    private const string BaseRoute = "Resumes";
    private const string APIVersion = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{APIVersion}/{BaseRoute}";

    /// <summary>
    /// Add the Resume Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Resume Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {

        // Update Endpoints
        Log.Information("Now adding Resume Model put endpoints");
        var singleEndpoint = app.MapPut($"{EndpointPrefix}/{{id}}", UpdateResumeModelAsync)
            .WithName("UpdateResumeModel")
            .Accepts<ResumeModel>(ContentType)
            .Produces<ResumeModel>(StatusCodes.Status200OK)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)                                  // you gave bad info
            .Produces(StatusCodes.Status404NotFound)                                        // could not find result to update
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(Tag);

        var multipleEndpoint = app.MapPut(EndpointPrefix, PutModelCollectionAsync)
            .Produces(StatusCodes.Status405MethodNotAllowed)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
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
    /// Search for and remove a specific Resume Model result from the database
    /// </summary>
    /// <param name="changes">The collection of changes to be applied to the newModel</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="linker">The web linker</param>
    /// <param name="http">the http context</param>
    /// <param name="token">Cancelation token</param>
    /// <returns>The updated copy of the Resume Model</returns>
    /// <response code="200">Update successful</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">Id was not found in the database</response>
    /// <response code="500">Something went wrong or the database does not exist</response>
    private static async Task<IResult> UpdateResumeModelAsync(UpdateResumeModelRequest changes,
        IResumeModelService service, IEducationDegreeModelService educationDegreeService, ICertificateModelService certificateService, IWorkExperienceModelService workExperienceService, IProjectModelService projectService, IAddressModelService addressService, IPhoneNumberModelService phoneNumberService, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Update Resume Model endpoint called by {username}", @username);

        var oldModel = await service.GetByIDAsync(changes.Id, token);
        if (oldModel.IsFail && ((Exception)((Error)oldModel).Exception).GetType() == typeof(NullReferenceException))
        {
            Log.Error("Address Model with ID:{id} does not exist", @changes.Id);
            return Results.NotFound();
        }
        if (oldModel.IsFail)
        {
            Log.Error(((Error)oldModel).ToException(), "Server issue encountered while trying to get all Resume Models from the database");
            return Results.Problem(detail: ((Error)oldModel).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }
        
        var requestedEducationDegreeModels = await educationDegreeService.GetAllAsync(new GetAllEducationDegreeModelsOptions {SpecificIds = changes.Degrees}, token);
        if (requestedEducationDegreeModels.IsFail)
        {
            Log.Error(((Error)requestedEducationDegreeModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedEducationDegreeModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedCertificateModels = await certificateService.GetAllAsync(new GetAllCertificateModelsOptions {SpecificIds = changes.Certificates}, token);
        if (requestedCertificateModels.IsFail)
        {
            Log.Error(((Error)requestedCertificateModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedCertificateModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedWorkExperienceModels = await workExperienceService.GetAllAsync(new GetAllWorkExperienceModelsOptions {SpecificIds = changes.WorkExperience}, token);
        if (requestedWorkExperienceModels.IsFail)
        {
            Log.Error(((Error)requestedWorkExperienceModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedWorkExperienceModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedProjectModels = await projectService.GetAllAsync(new GetAllProjectModelsOptions {SpecificIds = changes.Projects}, token);
        if (requestedProjectModels.IsFail)
        {
            Log.Error(((Error)requestedProjectModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedProjectModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedAddressModels = await addressService.GetAllAsync(new GetAllAddressModelsOptions {SpecificIds = changes.Addresses}, token);
        if (requestedAddressModels.IsFail)
        {
            Log.Error(((Error)requestedAddressModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedAddressModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedPhoneNumberModels = await phoneNumberService.GetAllAsync(new GetAllPhoneNumberModelsOptions {SpecificIds = changes.PhoneNumbers}, token);
        if (requestedPhoneNumberModels.IsFail)
        {
            Log.Error(((Error)requestedPhoneNumberModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedPhoneNumberModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var newModel = changes.MapToModelFromUpdateRequest((ResumeModel)oldModel!, username, (List<EducationDegreeModel>)requestedEducationDegreeModels, (List<CertificateModel>)requestedCertificateModels, (List<WorkExperienceModel>)requestedWorkExperienceModels, (List<ProjectModel>)requestedProjectModels, (List<AddressModel>)requestedAddressModels, (List<PhoneNumberModel>)requestedPhoneNumberModels);

        var validationResult = await service.ValidateModelForUpdate(newModel);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        var result = await service.UpdateAsync(newModel, (ResumeModel)oldModel, token);
        return result.Match(
            Succ =>
            {
                Log.Information("Resume Model with id {model.CommonIdentity} was successfully updated", @newModel.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetResumeModelByID", new { id = newModel.CommonIdentity });
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