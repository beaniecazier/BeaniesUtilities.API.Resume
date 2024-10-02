using BeaniesUtilities.Models.Resume;
using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Traits;

namespace Gay.TCazier.DatabaseParser.Endpoints;

public class ProjectEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Project Model";
    private const string BaseRoute = "Projects";

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IProjectService,ProjectService>();
    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Create Endpoints
        app.MapPost(BaseRoute, CreateProjectModelAsync)
            .WithName("CreateProject")
            .Accepts<EditibleProjectModel>(ContentType)
            .Produces<ProjectModel>(201)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        // Read Endpoints
        app.MapGet(BaseRoute, GetAllProjectModelsAsync)
            .WithName("GetAllProjectModels")
            .Produces(200)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/{{id}}", GetProjectModelByIDAsync)
            .WithName("GetProjectModelByID")
            .Produces(200)
            .Produces<IEnumerable<ValidationFailure>>(400)      // you gave bad info
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/entry{{entryId}}", GetProjectModelByEntryIDAsync)
            .WithName("GetProjectModelByEntryID")
            .Produces(200)
            .Produces<IEnumerable<ValidationFailure>>(400)      // you gave bad info
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);

        //app.MapGet($"{BaseRoute}/history{{id}}", GetHistroyOfProjectModelByIDAsync)
        //    .WithName("GetHistroyOfProjectModelByID")
        //    .WithTags(Tag);

        // Update Endpoints
        app.MapPut($"{BaseRoute}/{{id}}", UpdateProjectModelAsync)
            .WithName("UpdateProjectModel")
            .Accepts<EditibleProjectModel>(ContentType)
            .Produces<ProjectModel>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)      // you gave bad info
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);

        // Delete Endpoints
        app.MapDelete($"{BaseRoute}/{{id}}", DeleteAsync)
            .WithName("DeleteProjectModel")
            .Produces(200)
            .Produces(204)
            .Produces(404)                                      // could not find entry to update
            .WithTags(Tag);
    }

    #region Create
    
    /// <summary>
    /// Create a new Project Model
    /// </summary>
    /// <param name="newEntry">The parameters used to make the new Project Model</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="vaildator">Project Model value validator</param>
    /// <param name="linker">TODO</param>
    /// <param name="http">TODO</param>
    /// <param name="ctx">TODO</param>
    /// <returns>The newly created model</returns>
    /// <response code="201">Model was successfully created and added to the database</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    private static async Task<IResult> CreateProjectModelAsync(EditibleProjectModel newEntry,
        IProjectService service, IValidator<EditibleProjectModel> vaildator,
        LinkGenerator linker, HttpContext http, ResumeContext ctx)
    {
        var validationResult = await vaildator.ValidateAsync(newEntry);

        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var created = await service.CreateAsync(ctx, newEntry);

        //return Results.BadRequest(new List<ValidationFailure>
        //{
        //    new ("ID", "This Project already exists in the database." ),
        //});
        return created.Match(
            succ =>
            {
                var locationUri = linker.GetUriByName(http, "GetProjectModel", new { id = succ.CommonIdentity });
                return Results.Created(locationUri, succ.CommonIdentity);
            },
            fail => Results.BadRequest(fail.ToException()));
    }

    #endregion

    #region Read
    
    /// <summary>
    /// Retrieve all Project Models from the database
    /// </summary>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">TODO</param>
    /// <param name="nameSearchTerm">TODO</param>
    /// <param name="notesSearchTerm">TODO</param>
    /// <param name="start">TODO</param>
    /// <param name="end">TODO</param>
    /// <param name="isHidden">TODO</param>
    /// <param name="isDeleted">TODO</param>
    /// <param name="idLowerBound">TODO</param>
    /// <param name="idUpperBound">TODO</param>
    /// <param name="entryLowerBound">TODO</param>
    /// <param name="entryUpperBound">TODO</param>
    /// <returns>TODO</returns>
    /// <response code="200">Update successful</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">If the id is not found in the database</response>
    private static async Task<IResult> GetAllProjectModelsAsync(IProjectService service, ResumeContext ctx, string? nameSearchTerm,
        string? notesSearchTerm, DateTime? start, DateTime? end, bool? isHidden, bool? isDeleted,
        int? idLowerBound, int? idUpperBound, int? entryLowerBound, int? entryUpperBound)
    {
        var entries = await service.GetAllAsync(ctx);
        return entries.Match(
            Succ => Results.Ok(entries),
            Fail => Results.BadRequest(Fail.ToException()));
    }
    
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="id">TODO</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">TODO</param>
    /// <returns>TODO</returns>
    /// <response code="200">Update successful</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">If the id is not found in the database</response>
    private static async Task<IResult> GetProjectModelByIDAsync(int id, IProjectService service, ResumeContext ctx)
    {
        var entry = await service.GetByIDAsync(ctx, id);
        return entry.Match(
            Succ => Succ is null ? Results.NotFound() : Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }
    
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="entryId">TODO</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">TODO</param>
    /// <returns>TODO</returns>
    /// <response code="200">Update successful</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">If the id is not found in the database</response>
    private static async Task<IResult> GetProjectModelByEntryIDAsync(int entryId,IProjectService service, ResumeContext ctx)
    {
        var entry = await service.GetByEntryIDAsync(ctx, entryId);
        return entry.Match(
            Succ => Succ is null ? Results.NotFound() : Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    //private static async Task<IResult> GetHistroyOfProjectModelByIDAsync(int id, IProjectService service, ResumeContext ctx)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region Update
    
    /// <summary>
    /// Search for and remove a specific Project Model entry from the database
    /// </summary>
    /// <param name="id">TODO</param>
    /// <param name="changes">TODO</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="vaildator">TODO</param>
    /// <param name="ctx">TODO</param>
    /// <returns>The updated copy of the Project Model</returns>
    /// <response code="200">Update successful</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">If the id is not found in the database</response>
    private static async Task<IResult> UpdateProjectModelAsync(int id, EditibleProjectModel changes,
        IProjectService service, IValidator<EditibleProjectModel> vaildator, ResumeContext ctx)
    {
        var validationResult = await vaildator.ValidateAsync(changes);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var entry = await service.UpdateAsync(ctx, id, changes);
        return entry.Match(
            Succ => Results.Ok(Succ),
            Fail => Results.BadRequest(Fail.ToException()));
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete an Project Model by its id along with all of its history
    /// </summary>
    /// <param name="id">The id of the Project Model to delete form the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">The database context</param>
    /// <returns>Returns no content on a successful delete</returns>
    /// <response code="200">Delete worked, returns the last surviving copy</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">If the id is not found in the database</response>
    private static async Task<IResult> DeleteAsync(int id, IProjectService service, ResumeContext ctx)
    {
        var entry = await service.DeleteAsync(ctx, id);
        return entry.Match(
            Succ => Results.Ok(Succ),
            Fail =>
            {
                var ex = Fail.ToException();
                if (ex.Message == "204 - Nothing to delete") return Results.NotFound();
                if (ex.Message == $"404 - No entries with id {id}") return Results.NotFound();
                return Results.BadRequest(ex);
            });
    }

    /// <summary>
    /// Delete an Project Model by its id along with all of its history
    /// </summary>
    /// <param name="id">The id of the Project Model to delete form the database</param>
    /// <param name="service">The service class the serves this endpoint for database operations</param>
    /// <param name="ctx">The database context</param>
    /// <returns>Returns no content on a successful delete</returns>
    /// <response code="200">Delete worked, returns the last surviving copy</response>
    /// <response code="204">Nothing was found that need deleted</response>
    /// <response code="400">Something went wrong or the database does not exist</response>
    /// <response code="404">If the id is not found in the database</response>
    private static async Task<IResult> DeleteEntryAsync(int id, IProjectService service, ResumeContext ctx)
    {
        var entry = await service.DeleteEntryAsync(ctx, id);
        return entry.Match(
            Succ => Results.Ok(Succ),
            Fail =>
            {
                var ex = Fail.ToException();
                if (ex.Message == "204 - Nothing to delete") return Results.NotFound();
                if (ex.Message == $"404 - No entries with id {id}") return Results.NotFound();
                return Results.BadRequest(ex);
            });
    }

    #endregion
}