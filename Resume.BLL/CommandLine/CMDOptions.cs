using CommandLine;

namespace Gay.TCazier.Resume.BLL.CommandLine;

internal class CMDOptions
{
    // Omitting long name, defaults to name of property, ie "--verbose"
    [Option(
      Default = false,
      HelpText = "Prints all messages to standard output.")]
    public bool Verbose { get; set; }
}