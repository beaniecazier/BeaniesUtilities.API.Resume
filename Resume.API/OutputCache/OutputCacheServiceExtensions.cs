using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;

namespace Gay.TCazier.Resume.API.OutputCache;

public static class OutputCacheServiceExtensions
{
    public const int OutputCacheExpirationInMinutes = 1;

    public static IServiceCollection AddOutputAndResponseCacheing(this IServiceCollection services)
    {
        //services.AddResponseCaching();
        services.AddOutputCache(x =>
        {
            x.AddBasePolicy(c => c.Cache());

            x.AddPolicy(GetAddressModelEndpoint.EndpointPrefix, c =>
            {
                string[] queries = typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray();
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(queries)
                    .Tag(GetAddressModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetCertificateModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetCertificateModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetEducationDegreeModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetEducationDegreeModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetEducationInstitutionModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetEducationInstitutionModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetPersonModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetPersonModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetPhoneNumberModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetPhoneNumberModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetProjectModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetProjectModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetResumeModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetResumeModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetTechTagModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetTechTagModelEndpoint.EndpointPrefix);
            });

            x.AddPolicy(GetWorkExperienceModelEndpoint.EndpointPrefix, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(GetWorkExperienceModelEndpoint.EndpointPrefix);
            });
        });
        return services;
    }
}
