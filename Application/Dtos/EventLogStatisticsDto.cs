using System.Collections.Generic;

namespace Application.Dtos
{
    public class EventLogStatisticsDto
    {
        public int NumberOfEntries { get; set; }
        public Dictionary<string, int> NumberOfEntriesForEachLogLevel { get; set; }
        public Dictionary<string, int> NumberOfEntriesForEachEventSource { get; set; }
    }
}