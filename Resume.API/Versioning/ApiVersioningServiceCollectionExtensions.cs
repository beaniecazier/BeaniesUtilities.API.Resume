using Asp.Versioning;

namespace Gay.TCazier.Resume.API.Versioning;

public static class ApiVersioningServiceCollectionExtensions
{
    public static IServiceCollection AddApiVersioningSettings(this IServiceCollection services, ApiVersion defaultVersion)
    {
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = defaultVersion;
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.ReportApiVersions = true;
            x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
        }).AddApiExplorer();
        services.AddEndpointsApiExplorer();
        return services;
    }
}
