using CommandLine;
using Gay.TCazier.Resume.API.BackgroundServices;
using Gay.TCazier.Resume.API.OutputCache;
using Gay.TCazier.Resume.API.Swagger;

namespace Gay.TCazier.Resume.BLL.CommandLine;

public static class CommandLineApplicationExtension
{
    public static CMDOptions ParseCommandLine(string[] args)
    {
        var results = Parser.Default.ParseArguments<CMDOptions>(args);
        OutputCacheServiceExtensions.OutputCacheExpirationInMinutes = results.Value.CacheExpirationTimeInMinutes;
        //StartupBackgroundService.FakedStartupDurationInSeconds = results.Value.FakedStartupDurationInSeconds;

        ConfigureSwaggerOptions.ContactName = results.Value.ContactName;
        ConfigureSwaggerOptions.ContactURL = results.Value.ContactURL;
        ConfigureSwaggerOptions.ContactEmail = results.Value.ContactEmail;
        ConfigureSwaggerOptions.TermsOfServiceURL = results.Value.TermsOfServiceURL;

        return results.Value;
    }

    static void RunOptions(CMDOptions opts)
    {
        //handle options
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
        //handle errors
    }
}