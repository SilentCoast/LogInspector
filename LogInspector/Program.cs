using CommandLine;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace LogInspector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(options =>
            {
                // Override configuration values with command-line arguments
                if (!string.IsNullOrEmpty(options.LogFilePath))
                    configuration["LogFile"] = options.LogFilePath;
                if (!string.IsNullOrEmpty(options.OutputFilePath))
                    configuration["OutputFile"] = options.OutputFilePath;
                if (!string.IsNullOrEmpty(options.AddressStart))
                    configuration["AddressStart"] = options.AddressStart;
                if (!string.IsNullOrEmpty(options.AddressMask))
                    configuration["AddressMask"] = options.AddressMask;
                if (!string.IsNullOrEmpty(options.StartTime))
                    configuration["TimeStart"] = options.StartTime;
                if (!string.IsNullOrEmpty(options.EndTime))
                    configuration["TimeEnd"] = options.EndTime;
            });
            
            bool ArgumentsAreValid = true;

            string logFile = configuration["LogFile"];
            string outputFile = configuration["OutputFile"];
            string addressStart = configuration["AddressStart"];
            string addressMask = configuration["AddressMask"];
            string timeStart = configuration["TimeStart"];
            string timeEnd = configuration["TimeEnd"];

            if (!Validator.IsValidPath(logFile))
            {
                Console.WriteLine("LogFile is not valid");
                ArgumentsAreValid = false;
            }

            if (!Validator.IsValidPath(outputFile))
            {
                Console.WriteLine("OutputFile is not valid");
                ArgumentsAreValid = false;
            }

            IPAddress? startAddress = null;
            IPAddress? endAddress = null;
            try
            {
                startAddress = IPAddress.Parse(addressStart);
            }
            catch (FormatException)
            {
                //ignore addressStart in this case
            }

            try
            {
                int mask = int.Parse(addressMask);
                if (startAddress != null)
                {
                    byte[] startBytes = startAddress.GetAddressBytes();
                    startBytes[startBytes.Length - 1] += (byte)mask;
                    endAddress = new IPAddress(startBytes);
                }
            }
            catch(FormatException)
            {
                //ignor the mask in this case
            }

            DateTime startTime = default;
            try
            {
                startTime = DateTime.ParseExact(timeStart, "dd.MM.yyyy", null);
            }
            catch (FormatException)
            {
                Console.WriteLine("TimeStart not in the correct format");
                ArgumentsAreValid = false;
            }
            DateTime endTime = default;
            try
            {
                endTime = DateTime.ParseExact(timeEnd, "dd.MM.yyyy", null)
                    .AddDays(1)
                    .AddSeconds(-1);
            }
            catch (FormatException)
            {
                Console.WriteLine("TimeEnd not in the correct format");
                ArgumentsAreValid = false;
            }

            if (ArgumentsAreValid)
            {
                Console.WriteLine($"Log file path: {logFile}");
                Console.WriteLine($"Output file path: {outputFile}");
                Console.WriteLine($"Address start: {addressStart}");
                Console.WriteLine($"Address mask: {addressMask}");
                Console.WriteLine($"Start time: {timeStart}");
                Console.WriteLine($"End time: {timeEnd}");
                Console.WriteLine();

                if(LogAnalyzer.FilterLogEntries(out List<LogFilterResult> results,logFile, startTime, endTime, startAddress, endAddress))
                {
                    if (results != null)
                    {
                        using (StreamWriter writer = new StreamWriter(outputFile))
                        {
                            foreach (LogFilterResult result in results)
                            {
                                writer.WriteLine($"{result.IPAddress}: {result.CallAmount} call(s) from ({result.StartTime:dd.MM.yyyy HH:mm:ss}) to ({result.EndTime:dd.MM.yyyy HH:mm:ss})");
                                writer.WriteLine("Call times:");
                                foreach (LogEntry entry in result.LogEntries)
                                {
                                    writer.WriteLine(entry.Timestamp);
                                }
                                writer.WriteLine();
                            }
                        }
                    }
                    Console.WriteLine("Logs filtered");
                }
                else
                {
                    Console.WriteLine("Couldn't process LogFile, consider checking if it is valid");
                }
            }
            else
            {
                Console.WriteLine("Cannot proceed, arguments are not valid");
            }

            Console.ReadKey();
        }
    }
}
