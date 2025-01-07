using Azure;
using BeaniesUtilities.APIUtilities.Endpoints;
using Gay.TCazier.DatabaseParser.Endpoints.Interfaces;
using Gay.TCazier.Resume.API.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Gay.TCazier.Resume.API.Endpoints.V1.Health;

public class LivenessCheckEndpoint : IEndpoints, IHealthCheck
{
    private const string _contentType = "application/json";

    private const string _tag = "liveness";
    private const string _baseRoute = "_health";
    private const string _apiVersion = "v1";
    private const double _versionNumber = 1.0;

    private const int _port = 5003;
    private static readonly string _hostAddress = "*";

    /// <summary>
    /// 
    /// </summary>
    public static string Tag => _tag;

    /// <summary>
    /// 
    /// </summary>
    public static string EndpointPrefix => $"{_apiVersion}/{_baseRoute}";

    /// <summary>
    /// Add the Address Model Service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// Map all Address Model endpoints with correct settings
    /// </summary>
    /// <param name="app"></param>
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        var singleEndpoint = app.MapHealthChecks($"{EndpointPrefix}/{_tag}", new HealthCheckOptions
        {
            ResponseWriter = HealthReportWriter.WriteResponse,
            AllowCachingResponses = false,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            Predicate = healthCheck => healthCheck.Tags.Contains(_tag),
        })
        .WithApiVersionSet(APIVersioning.VersionSet)
        .HasApiVersion(_versionNumber)
        .RequireHost($"{_hostAddress}:{_port}");

        //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        //{
        //    singleEndpoint.AllowAnonymous();
        //}
        //else
        //{
        //    singleEndpoint.RequireAuthorization(AuthConstants.AdminUserPolicyName);
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            return Task.FromResult(HealthCheckResult.Healthy("A healthy result"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, $"An unhealthy result, {ex.Message}"));
        }
    }
}