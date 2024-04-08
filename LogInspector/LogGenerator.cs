
namespace LogInspector
{
    public static class LogGenerator
    {
        /// <summary>
        /// Generates random logs for testing purposes, entries sorted by time
        /// </summary>
        /// <param name="logFileName">full path</param>
        public static void GenerateLog(string logFileName)
        {
            using (StreamWriter writer = new StreamWriter(logFileName))
            {
                DateTime currentTime = DateTime.Now.AddYears(-3); // Start from three years ago
                Random rand = new Random();

                // Generate log entries
                var logEntries = Enumerable.Range(1, 1000)
                    .Select(i =>
                    {
                        string ipAddress = $"192.168.0.{rand.Next(1, 16)}";
                        DateTime timestamp = currentTime.AddMinutes(rand.Next(1, 365 * 24 * 60 * 3));
                        return $"{ipAddress}:{timestamp:yyyy-MM-dd HH:mm:ss}";
                    })
                    .OrderBy(entry => DateTime.Parse(entry.Substring(entry.IndexOf(':')+1))); // Sort entries by timestamp

                // Write sorted log entries to the file
                foreach (var entry in logEntries)
                {
                    writer.WriteLine(entry);
                }
            }

            Console.WriteLine($"Generated log file: {logFileName}");
        }
    }
}
