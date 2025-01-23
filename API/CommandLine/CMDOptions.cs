using CommandLine;

namespace Gay.TCazier.Resume.BLL.CommandLine;

public class CMDOptions
{

    //[Option("db", Required = true)]
    //public string DBConnStr { get; set; }

    //[Option('c', "dbconn", Required = true)]
    //public string BaseUrl {  get; set; }

    [Option("versions", Required = true, Separator = ' ')]
    public IEnumerable<string> VersionsToLoad { get; set; }

    [Option('h', "health_host", Required = true)]
    public string HealthChecksHostAddress { get; set; }



    // Omitting long name, defaults to name of property, ie "--verbose"
    [Option(
        'v',
        "versbose",
        Default = false,
        HelpText = "Prints all messages to standard output.")]
    public bool Verbose { get; set; }





    [Option("ready", Default = 5050)]
    public int ReadyCheckPort { get; set; }

    [Option("liveness", Default = 5050)]
    public int LivenessCheckPort { get; set; }

    [Option("startup", Default = 5050)]
    public int StartupCheckPort { get; set; }

    [Option(Default = 5000)]
    public int HttpPort { get; set; }

    [Option(Default = 5001)]
    public int HttpsPort { get; set; }

    [Option('c', Default = 60)]
    public int CacheExpirationTimeInMinutes { get; set; }

    //[Option("startuptime", Default = 10)]
    //public int FakedStartupDurationInSeconds { get; set; }

    [Option(Default = "Tiabeanie Cazier")]
    public string ContactName { get; set; }

    [Option(Default = "beanieroxiicazier@gmail.com")]
    public string ContactEmail { get; set; }

    [Option(Default = "https://tcazier.gay/contact")]
    public string ContactURL { get; set; }

    [Option(Default = "https://tcazier.gay/terms")]
    public string TermsOfServiceURL { get; set; }

    //loging information like min level

}