using System.Net;

namespace LogInspector
{
    public class LogEntry
    {
        public IPAddress IPAddress { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
