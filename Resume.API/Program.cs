using FluentValidation;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
//    option =>
//{
//    option.SwaggerDoc("ResumeV1", new OpenApiInfo()
//    {
//        Title = "Resume SQL DB Operations For Version 1 Of The Schema",
//        Version = "v1",
//        Description = "",
//        //Contact = ,
//        //Extensions = { },
//        //License = ,
//        //TermsOfService = ,
//    });
//    //option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//    //{
//    //    In = ParameterLocation.Header,
//    //    Description = "Please enter a valid token",
//    //    Name = "Authorization",
//    //    Type = SecuritySchemeType.Http,
//    //    BearerFormat = "JWT",
//    //    Scheme = "Bearer",
//    //    //Extensions = new[] { },
//    //    //Flows = new[] { },
//    //    //OpenIdConnectUrl = ,
//    //    //Reference = new OpenApiReference(),
//    //    //UnresolvedReference = ,
//    //});
//    //option.AddSecurityRequirement(new OpenApiSecurityRequirement
//    //{
//    //    {
//    //        new OpenApiSecurityScheme
//    //        {
//    //            Reference = new OpenApiReference
//    //            {
//    //                Type=ReferenceType.SecurityScheme,
//    //                Id="Bearer",
//    //            }
//    //        },
//    //        new string[]{}
//    //    }
//    //});
//}
);

// Add a DbContext here
var connStr = builder.Configuration.GetConnectionString("Dev");
builder.Services.AddDbContext<ResumeContext>(options =>
{
    options.UseSqlServer(connStr);
});

builder.Services.AddEndpoints<Program>(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Check if the DB was migrated
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ResumeContext>();

//dirty hack time to nail down the db schema
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

//var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
//if (pendingMigrations.Any())
//    throw new Exception("Database is not fully migrated for MoviesContext.");

app.UseSwagger();
app.UseSwaggerUI(
//    c =>
//{
//    c.SwaggerEndpoint("./swagger/v1/swagger.json", "ResumeV1");
//}
);

app.UseEndpoints<Program>();

app.Run();
