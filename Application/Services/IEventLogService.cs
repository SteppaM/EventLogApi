using System.Collections.Generic;
using Application.Dtos;

namespace Application.Services
{
    public interface IEventLogService
    {
        int AddLogEntry(LogEntryDto logEntryDto);
        IReadOnlyCollection<LogEntryDto> GetLogEntries(LogEntryQueryParameters queryParameters);
        int RemoveLogEntries(List<int> entryIds);
        EventLogStatisticsDto GetStatistics();
    }
}