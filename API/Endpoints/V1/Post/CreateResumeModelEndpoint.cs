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
/// The collection of Endpoints for the Resume Model in API
/// </summary>
[ApiVersion(1.0)]
public class CreateResumeModelEndpoint : IEndpoints
{
    private const string ContentType = "application/json";

    /// <summary>
    /// Add the Resume Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Resume Model Endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        Log.Information("Now adding Resume Model post Endpoints");
        var singleEndpoint = app.MapPost(ResumeModelEndpoints.Post, CreateResumeModelAsync)
            .WithName("CreateResume")
            .Accepts<ResumeModel>(ContentType)
            .Produces<ResumeModel>(StatusCodes.Status201Created)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(APIVersioning.VersionSet)
            .HasApiVersion(1.0)
            .WithTags(ResumeModelEndpoints.Tag);

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
    /// Create a new Resume Model
    /// </summary>
    /// <param name="request">The parameters used to make the new Resume Model</param>
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
    private static async Task<IResult> CreateResumeModelAsync(CreateResumeModelRequest request,
        IResumeModelService service, IEducationDegreeModelService educationDegreeService, ICertificateModelService certificateService, IWorkExperienceModelService workExperienceService, IProjectModelService projectService, IAddressModelService addressService, IPhoneNumberModelService phoneNumberService,
        IOutputCacheStore outputCacheStore, LinkGenerator linker, HttpContext http, CancellationToken token)
    {
        //string username = http.User.Identity!.Name??"fuck me....";
        string username = "Tiabeanie";

        Log.Information("Create Resume Model Endpoint called by {username}", @username);

        int id = await service.GetNextAvailableId();
        
        var requestedEducationDegreeModels = await educationDegreeService.GetAllAsync(new GetAllEducationDegreeModelsOptions {SpecificIds = request.Degrees}, token);
        if (requestedEducationDegreeModels.IsFail)
        {
            Log.Error(((Error)requestedEducationDegreeModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedEducationDegreeModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedCertificateModels = await certificateService.GetAllAsync(new GetAllCertificateModelsOptions {SpecificIds = request.Certificates}, token);
        if (requestedCertificateModels.IsFail)
        {
            Log.Error(((Error)requestedCertificateModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedCertificateModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedWorkExperienceModels = await workExperienceService.GetAllAsync(new GetAllWorkExperienceModelsOptions {SpecificIds = request.WorkExperience}, token);
        if (requestedWorkExperienceModels.IsFail)
        {
            Log.Error(((Error)requestedWorkExperienceModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedWorkExperienceModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

        var requestedProjectModels = await projectService.GetAllAsync(new GetAllProjectModelsOptions {SpecificIds = request.Projects}, token);
        if (requestedProjectModels.IsFail)
        {
            Log.Error(((Error)requestedProjectModels).ToException(), "Server issue encountered while trying to query for the list of requested ***PLURALIZE***");
            return Results.Problem(detail: ((Error)requestedProjectModels).ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

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

        var model = request.MapToModelFromCreateRequest(id, username, (List<EducationDegreeModel>)requestedEducationDegreeModels, (List<CertificateModel>)requestedCertificateModels, (List<WorkExperienceModel>)requestedWorkExperienceModels, (List<ProjectModel>)requestedProjectModels, (List<AddressModel>)requestedAddressModels, (List<PhoneNumberModel>)requestedPhoneNumberModels);
        var validationResult = await service.ValidateModelForCreation(model);
        if (validationResult.Count() > 0)
        {
            foreach (var item in validationResult) Log.Error(item.ToString());
            return Results.BadRequest(validationResult);
        }

        var created = await service.CreateAsync(model, token);
        if(!created.IsFail) await outputCacheStore.EvictByTagAsync(ResumeModelEndpoints.Tag, token);
        return created.Match(
            succ =>
            {
                Log.Information("Resume Model Created with id {model.CommonIdentity}", @model.CommonIdentity);
                var locationUri = linker.GetUriByName(http, "GetResumeModelByID", new { id = model.CommonIdentity });
                return Results.Created(locationUri, succ);
            },
            fail =>
            {
                Log.Error(fail.ToException(), "Server issue encountered while trying to add a new Resume Model to the database");
                return Results.Problem(detail: fail.ToException().ToString(), statusCode: StatusCodes.Status500InternalServerError);
            }
        );
    }

    #endregion
}