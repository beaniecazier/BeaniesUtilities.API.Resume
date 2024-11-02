using FluentValidation;
using Gay.TCazier.Resume.BLL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Gay.TCazier.Resume.BLL;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ResumeContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }

    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        //builder.Services.AddAuthentication(x =>
        //{
        //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddJwtBearer(x =>
        //{
        //    x.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        IssuerSigningKey = new SymmetricSecurityKey(
        //            Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
        //        ValidateIssuerSigningKey = true,
        //        ValidateLifetime = true,
        //        ValidIssuer = config["Jwt:Issuer"],
        //        ValidAudience = config["Jwt:Audience"],
        //        ValidateIssuer = true,
        //        ValidateAudience = true
        //    };
        //});

        //builder.Services.AddAuthorization(x =>
        //{
        //    // x.AddPolicy(AuthConstants.AdminUserPolicyName, 
        //    //     p => p.RequireClaim(AuthConstants.AdminUserClaimName, "true"));

        //    x.AddPolicy(AuthConstants.AdminUserPolicyName,
        //        p => p.AddRequirements(new AdminAuthRequirement(config["ApiKey"]!)));

        //    x.AddPolicy(AuthConstants.TrustedMemberPolicyName,
        //    p => p.RequireAssertion(c =>
        //            c.User.HasClaim(m => m is { Type: AuthConstants.AdminUserClaimName, Value: "true" }) ||
        //            c.User.HasClaim(m => m is { Type: AuthConstants.TrustedMemberClaimName, Value: "true" })));
        //});
        return services;
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

    public static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddOutputCacheing(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services)
    {
        return services;
    }
}
