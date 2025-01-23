using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;

namespace Gay.TCazier.Resume.API.OutputCache;

public static class OutputCacheServiceExtensions
{
    public static int OutputCacheExpirationInMinutes = 1;

    public static IServiceCollection AddOutputAndResponseCacheing(this IServiceCollection services)
    {
        //services.AddResponseCaching();
        services.AddOutputCache(x =>
        {
            x.AddBasePolicy(c => c.Cache());

            x.AddPolicy(AddressModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllAddressModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(AddressModelEndpoints.Tag);
            });

            x.AddPolicy(CertificateModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllCertificateModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(CertificateModelEndpoints.Tag);
            });

            x.AddPolicy(EducationDegreeModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllEducationDegreeModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(EducationDegreeModelEndpoints.Tag);
            });

            x.AddPolicy(EducationInstitutionModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllEducationInstitutionModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(EducationInstitutionModelEndpoints.Tag);
            });

            x.AddPolicy(PersonModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllPersonModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(PersonModelEndpoints.Tag);
            });

            x.AddPolicy(PhoneNumberModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllPhoneNumberModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(PhoneNumberModelEndpoints.Tag);
            });

            x.AddPolicy(ProjectModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllProjectModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(ProjectModelEndpoints.Tag);
            });

            x.AddPolicy(ResumeModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllResumeModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(ResumeModelEndpoints.Tag);
            });

            x.AddPolicy(TechTagModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllTechTagModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(TechTagModelEndpoints.Tag);
            });

            x.AddPolicy(WorkExperienceModelEndpoints.Tag, c =>
            {
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(OutputCacheExpirationInMinutes))
                    .SetVaryByQuery(typeof(GetAllWorkExperienceModelsRequest).GetProperties().Select(p => p.Name).ToArray())
                    .Tag(WorkExperienceModelEndpoints.Tag);
            });
        });
        return services;
    }
}
