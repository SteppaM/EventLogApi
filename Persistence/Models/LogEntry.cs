using System;

namespace Persistence.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string EventSourceName { get; set; }
        public string EventDescription { get; set; }
        public DateTime TimeStamp { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}