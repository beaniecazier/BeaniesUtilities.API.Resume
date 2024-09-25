using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services;
using Gay.TCazier.DatabaseParser.Services.Interfaces;

namespace Gay.TCazier.DatabaseParser.Endpoints;

public class WorkExperienceEndpoints : IEndpoints
{
    private const string ContentType = "";
    private const string Tag = "";
    private const string BaseRoute = "work-experiences";

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IWorkExperienceService, WorkExperienceService>();
    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreateWorkExperienceModel);
    }

    private static async Task<IResult> CreateWorkExperienceModel(EditibleWorkExperienceModel newEntry, IWorkExperienceService service, IValidator<EditibleWorkExperienceModel> vaildator)
    {
        var validationResult = await vaildator.ValidateAsync(newEntry);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var created = await service.CreateAsync(newEntry);

        if (created.IsFail)
        {
            return Results.BadRequest(new List<ValidationFailure>
            {
                new ("ID", "This address already exists in the database." ),
            });
        }

        var model = created.ToList()[0];

        return Results.Created($"/{BaseRoute}/{model.CommonIdentity}", model.CommonIdentity);
    }
}
