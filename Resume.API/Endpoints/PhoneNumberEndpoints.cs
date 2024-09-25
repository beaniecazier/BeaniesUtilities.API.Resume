﻿using FluentValidation;
using FluentValidation.Results;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services;
using Gay.TCazier.DatabaseParser.Services.Interfaces;

namespace Gay.TCazier.DatabaseParser.Endpoints;

public class PhoneNumberEndpoints : IEndpoints
{
    private const string ContentType = "";
    private const string Tag = "";
    private const string BaseRoute = "phone-numbers";

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPhoneNumberService, PhoneNumberService>();
    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreatePhoneNumberModel);
    }

    private static async Task<IResult> CreatePhoneNumberModel(EditiblePhoneNumberModel newEntry, IPhoneNumberService service, IValidator<EditiblePhoneNumberModel> vaildator)
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