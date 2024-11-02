using Gay.TCazier.DatabaseParser.Endpoints.Extensions;
using Gay.TCazier.Resume.BLL;
using Gay.TCazier.Resume.BLL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

Serilog.ILogger logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    //.WriteTo.File("log.txt", rollingInterval:RollingInterval.Day, rollOnFileSizeLimit:true)
    //.Destructure.ByTransforming<AddressModel> (x => new EditibleAddressModel(x, x.CommonIdentity))
    .CreateLogger();
Log.Logger = logger;
builder.Host.UseSerilog();

builder.Services.AddJsonConfigurationOptions();

builder.Services.AddSecurity();

//builder.Services.AddScoped<ApiKeyAuthFilter>();

//builder.Services.AddApiVersioning(x =>
//{
//    x.DefaultApiVersion = new ApiVersion(1.0);
//    x.AssumeDefaultVersionWhenUnspecified = true;
//    x.ReportApiVersions = true;
//    x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
//}).AddApiExplorer();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOutputCacheing();

//builder.Services.AddHealthChecks()
//    .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
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
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddApplication();

var connStr = builder.Configuration.GetConnectionString("Dev");
if (string.IsNullOrWhiteSpace(connStr))
{
    Log.Fatal("");
    return;
}
builder.Services.AddDatabase(connStr);

builder.Services.AddEndpoints<Program>(builder.Configuration);

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
});

app.UseEndpoints<Program>();

app.Run();

await Log.CloseAndFlushAsync();