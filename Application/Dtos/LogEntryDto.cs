using System;
using Persistence.Models;

namespace Application.Dtos
{
    public class LogEntryDto
    {
        public string EventSourceName { get; set; }
        public string EventDescription { get; set; }
        public LogLevel LogLevel { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}