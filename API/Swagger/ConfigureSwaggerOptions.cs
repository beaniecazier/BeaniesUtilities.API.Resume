using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gay.TCazier.Resume.API.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IHostEnvironment _environment;

    public static string ContactName = "Unassigned Value";
    public static string ContactURL = "Unassigned Value";
    public static string ContactEmail = "Unassigned Value";
    public static string TermsOfServiceURL = "Unassigned Value";

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IHostEnvironment environment)
    {
        _provider = provider;
        _environment = environment;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = _environment.ApplicationName,
                    Version = description.ApiVersion.ToString(),
                    Description = "Resume API Version Documentation",
                    Contact = new OpenApiContact
                    {
                        Name = ContactEmail,
                        Url = new Uri(ContactURL),
                        Email = ContactEmail,
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://mit-license.org/")
                    },
                    TermsOfService = new Uri(TermsOfServiceURL),
                });
        }
    }
}