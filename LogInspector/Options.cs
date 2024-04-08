using CommandLine;

namespace LogInspector
{
    public class Options
    {
        [Option("file-log", Required = false, HelpText = "Path to the log file.")]
        public string LogFilePath { get; set; }

        [Option("file-output", Required = false, HelpText = "Path to the output file.")]
        public string OutputFilePath { get; set; }

        [Option("address-start", Required = false, HelpText = "Lower bound of the address range.")]
        public string AddressStart { get; set; }

        [Option("address-mask", Required = false, HelpText = "Address mask.")]
        public string AddressMask { get; set; }

        [Option("time-start", Required = false, HelpText = "Start time.")]
        public string StartTime { get; set; }

        [Option("time-end", Required = false, HelpText = "End time.")]
        public string EndTime { get; set; }
    }
}
