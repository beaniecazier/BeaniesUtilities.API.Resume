using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Gay.TCazier.Resume.API.Swagger;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAndAddSwagger(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();

            options.OperationFilter<SwaggerDefaultValues>();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            var keycloakScopes = new Dictionary<string, string>
            {
                { "openid", "openid" },
                { "profile", "profile" }
            };

            var keycloakFlow = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(config["Keycloak:AuthorizationURL"]!),
                Scopes = keycloakScopes
            };

            var oauthFlows = new OpenApiOAuthFlows
            {
                Implicit = keycloakFlow,
            };

            options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = oauthFlows
            });

            var securityRequirement = MakeNewOpenApiSecurityRequirement();
            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }

    private static OpenApiSecurityRequirement MakeNewOpenApiSecurityRequirement()
    {
        var reference = new OpenApiReference
        {
            Id = "Keycloak",
            Type = ReferenceType.SecurityScheme,
        };

        var securityScheme = new OpenApiSecurityScheme
        {
            Reference = reference,
            In = ParameterLocation.Header,
            Name = "Bearer",
            Scheme = "Bearer",
        };

        return new OpenApiSecurityRequirement
        {
            {
                securityScheme,
                []
            }
        };
    }
}
