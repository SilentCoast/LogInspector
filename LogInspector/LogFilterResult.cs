using System.Net;

namespace LogInspector
{
    public class LogFilterResult
    {
        public IPAddress IPAddress { get; set; }
        public int CallAmount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<LogEntry> LogEntries { get; set; }
    }
}
