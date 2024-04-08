using System.Net;

namespace LogInspector
{
    public static class LogAnalyzer
    {
        public static bool FilterLogEntries(out List<LogFilterResult> result, string logFile, DateTime startTime, DateTime endTime, IPAddress? startAddress = null, IPAddress? endAddress = null)
        {
            try
            {
                //check if address checks will be necessery
                bool CheckStartAddress = startAddress is not null;
                bool CheckEndAddress = endAddress is not null;

                var filteredEntries = new List<LogEntry>();
                foreach (string entry in File.ReadLines(logFile))
                {
                    // Parse entry
                    int colonIndex = entry.IndexOf(':');
                    if (colonIndex == -1 || colonIndex == entry.Length - 1)
                    {
                        throw new ArgumentException();
                    }

                    // Extract IP address and timestamp parts
                    string ipAddress = entry.Substring(0, colonIndex);
                    string timestampString = entry.Substring(colonIndex + 1);
                    DateTime timestamp = DateTime.ParseExact(timestampString, "yyyy-MM-dd HH:mm:ss", null);

                    // Check if IP address is within the specified range
                    IPAddress entryIPAddress = IPAddress.Parse(ipAddress);
                    if (CheckStartAddress)
                    {
                        if (CheckEndAddress)
                        {
                            if (!Validator.IsIPAddressInRange(entryIPAddress, startAddress, endAddress))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (!Validator.IsIPAddressInRange(entryIPAddress, startAddress))
                            {
                                continue;
                            }
                        }
                    }

                    // Check if timestamp is within the specified range
                    if (timestamp < startTime || timestamp > endTime)
                    {
                        continue;
                    }

                    // If both conditions are satisfied, add entry to the filtered list
                    filteredEntries.Add(new LogEntry { IPAddress = entryIPAddress, Timestamp = timestamp });
                }
                // Group the filtered entries by IPAddress
                var groupedEntries = filteredEntries
                    .GroupBy(entry => entry.IPAddress)
                    .OrderBy(group => group.Key,new IPAddressComparer())
                    .Select(group => new LogFilterResult
                    {
                        IPAddress = group.Key,
                        CallAmount = group.Count(),
                        StartTime = startTime,
                        EndTime = endTime,
                        LogEntries = group.Select(entry => new LogEntry
                        {
                            IPAddress = entry.IPAddress,
                            Timestamp = entry.Timestamp,
                        }).ToList()
                    })
                    .ToList();

                result = groupedEntries;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}
