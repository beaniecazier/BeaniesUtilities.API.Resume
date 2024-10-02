using FluentValidation;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Resume SQL DB Operations For Version 1 Of The Schema",
        Version = "v1",
        Description = "Describing things here",
        Contact = new OpenApiContact
        {
            Name = "Name",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Eaxmple License",
            Url = new Uri("https://example.com/license")
        },
        TermsOfService = new Uri("https://example.com/terms"),
    });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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

app.UseSwagger(options =>
{
    options.RouteTemplate = "resume/swagger/{documentname}/swagger.json";
});

app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "v1");
        c.RoutePrefix = "resume/swagger";
    }
);

app.UseEndpoints<Program>();

app.Run();
