using BeaniesUtilities.Models.Resume;
using FluentValidation;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Repositories.Interfaces;
using Gay.TCazier.Resume.BLL.Repositories;
using Gay.TCazier.Resume.BLL.Services.Interfaces;
using Gay.TCazier.Resume.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Gay.TCazier.Resume.BLL;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Log.Information("Adding Address Model Service to the DI Container");
        services.AddSingleton<IAddressModelRepository, AddressModelInMemRepository>();
        services.AddSingleton<IAddressModelService, AddressModelService>();

        Log.Information("Adding Certificate Model Service to the DI Container");
        services.AddSingleton<ICertificateModelRepository, CertificateModelInMemRepository>();
        services.AddSingleton<ICertificateModelService, CertificateModelService>();

        Log.Information("Adding EducationDegree Model Service to the DI Container");
        services.AddSingleton<IEducationDegreeModelRepository, EducationDegreeModelInMemRepository>();
        services.AddSingleton<IEducationDegreeModelService, EducationDegreeModelService>();

        Log.Information("Adding EducationInstitution Model Service to the DI Container");
        services.AddSingleton<IEducationInstitutionModelRepository, EducationInstitutionModelInMemRepository>();
        services.AddSingleton<IEducationInstitutionModelService, EducationInstitutionModelService>();

        Log.Information("Adding Person Model Service to the DI Container");
        services.AddSingleton<IPersonModelRepository, PersonModelInMemRepository>();
        services.AddSingleton<IPersonModelService, PersonModelService>();

        Log.Information("Adding PhoneNumber Model Service to the DI Container");
        services.AddSingleton<IPhoneNumberModelRepository, PhoneNumberModelInMemRepository>();
        services.AddSingleton<IPhoneNumberModelService, PhoneNumberModelService>();

        Log.Information("Adding Project Model Service to the DI Container");
        services.AddSingleton<IProjectModelRepository, ProjectModelInMemRepository>();
        services.AddSingleton<IProjectModelService, ProjectModelService>();

        Log.Information("Adding Resume Model Service to the DI Container");
        services.AddSingleton<IResumeModelRepository, ResumeModelInMemRepository>();
        services.AddSingleton<IResumeModelService, ResumeModelService>();

        Log.Information("Adding TechTag Model Service to the DI Container");
        services.AddSingleton<ITechTagModelRepository, TechTagModelInMemRepository>();
        services.AddSingleton<ITechTagModelService, TechTagModelService>();

        Log.Information("Adding WorkExperience Model Service to the DI Container");
        services.AddSingleton<IWorkExperienceModelRepository, WorkExperienceModelInMemRepository>();
        services.AddSingleton<IWorkExperienceModelService, WorkExperienceModelService>();

        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager config)
    {
        var connStr = config.GetConnectionString("Dev");
        if (string.IsNullOrWhiteSpace(connStr))
        {
            Log.Fatal("");
            return services;
        }

        services.AddDbContext<ResumeContext>(options =>
        {
            options.UseSqlServer(connStr);
        });
        return services;
    }

    public static void AddLoggingWithSerilog(this WebApplicationBuilder builder, ConfigurationManager config)
    {
        Serilog.ILogger logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            //.WriteTo.File("log.txt", rollingInterval:RollingInterval.Day, rollOnFileSizeLimit:true)

            //.Destructure.ByTransforming<AddressModel> (x => new EditibleAddressModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<CertificateModel> (x => new EditibleCertificateModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<EducationDegreeModel> (x => new EditibleEducationDegreeModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<EducationInstitutionModel> (x => new EditibleEducationInstitutionModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<PersonModel> (x => new EditiblePersonModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<PhoneNumberModel> (x => new EditiblePhoneNumberModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<ProjectModel> (x => new EditibleProjectModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<ResumeModel> (x => new EditibleResumeModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<TechTagModel> (x => new EditibleTechTagModel(x, x.CommonIdentity))

            //.Destructure.ByTransforming<WorkExperienceModel> (x => new EditibleWorkExperienceModel(x, x.CommonIdentity))
            .CreateLogger();
        Log.Logger = logger;
        builder.Host.UseSerilog();
    }

    public static IServiceCollection AddJsonConfigurationOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });
        return services;
    }
}
