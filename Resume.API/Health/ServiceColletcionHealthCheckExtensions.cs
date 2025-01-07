using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.API.Endpoints.V1.Health;

namespace Gay.TCazier.Resume.API.Health;

public static class ServiceColletcionHealthCheckExtensions
{
    public static IServiceCollection AddHealthCheckServices(this IServiceCollection services)
    {
        services.AddSingleton<StartupCheckEndpoint>();
        services.AddSingleton<ReadinessCheckEndpoint>();
        services.AddSingleton<LivenessCheckEndpoint>();

        services.AddHealthChecks()
                .AddDbContextCheck<ResumeContext>()
                .AddCheck<StartupCheckEndpoint>(StartupCheckEndpoint.Tag, tags: new[] { StartupCheckEndpoint.Tag })
                .AddCheck<ReadinessCheckEndpoint>(ReadinessCheckEndpoint.Tag, tags: new[] { ReadinessCheckEndpoint.Tag })
                .AddCheck<LivenessCheckEndpoint>(LivenessCheckEndpoint.Tag, tags: new[] { LivenessCheckEndpoint.Tag });
        //    .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);
        return services;
    }
}
