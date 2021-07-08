using System;
using System.Collections.Generic;
using System.Linq;
using Application.Dtos;
using Application.Exceptions;
using Persistence;
using Persistence.Models;

namespace Application.Services
{
    public class EventLogService : IEventLogService
    {
        public int AddLogEntry(LogEntryDto logEntryDto)
        {
            var logEntry = new LogEntry
            {
                EventSourceName = logEntryDto.EventSourceName,
                EventDescription = logEntryDto.EventDescription,
                TimeStamp = logEntryDto.TimeStamp,
                LogLevel = logEntryDto.LogLevel
            };
            var entityEntry = _eventLogContext.LogEntries.Add(logEntry);
            _eventLogContext.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public IReadOnlyCollection<LogEntryDto> GetLogEntries(LogEntryQueryParameters queryParameters)
        {
            if (queryParameters.FromDateTime > queryParameters.ToDateTime)
            {
                throw new QueryParameterException("End date must be greater than start date");
            }
            
            var entries = _eventLogContext.LogEntries
                .Where(e =>
                    (queryParameters.FromDateTime <= e.TimeStamp && e.TimeStamp <= queryParameters.ToDateTime)
                    //If LogLevels is empty, return all entries;
                    && (queryParameters.LogLevels.Count == 0 || queryParameters.LogLevels.Contains(e.LogLevel))
                    && (e.EventSourceName.Contains(queryParameters.SearchString,
                        StringComparison.InvariantCultureIgnoreCase)));

            var dtos = entries
                .Select(e => new LogEntryDto
                {
                    EventDescription = e.EventDescription,
                    EventSourceName = e.EventSourceName,
                    LogLevel = e.LogLevel,
                    TimeStamp = e.TimeStamp
                }).ToList();

            return dtos;
        }
        
        public int RemoveLogEntries(List<int> entryIds)
        {
            var entries = _eventLogContext.LogEntries
                .Where(e => entryIds.Contains(e.Id));
            var count = entries.Count();
            _eventLogContext.LogEntries.RemoveRange(entries);
            _eventLogContext.SaveChanges();
            return count;
        }

        public EventLogStatisticsDto GetStatistics()
        {
            var numberOfEntries = _eventLogContext.LogEntries.Count();

            var numberOfEntriesForEachLogLevel = new Dictionary<string, int>();
            foreach (var logLevel in (LogLevel[]) Enum.GetValues(typeof(LogLevel)))
            {
                var entriesCount = _eventLogContext.LogEntries
                    .Count(e => e.LogLevel == logLevel);
                numberOfEntriesForEachLogLevel.Add(logLevel.ToString(), entriesCount);
            }
            
            var numberOfEntriesForEachEventSource = new Dictionary<string, int>();
            var grouped = _eventLogContext.LogEntries
                .GroupBy(e => e.EventSourceName)
                .Select(e => new Tuple<string, int>(e.Key, e.Count()));
            foreach (var (key, count) in grouped)
            {
                numberOfEntriesForEachEventSource.Add(key, count);
            }

            var statistics = new EventLogStatisticsDto
            {
                NumberOfEntries = numberOfEntries,
                NumberOfEntriesForEachEventSource = numberOfEntriesForEachEventSource,
                NumberOfEntriesForEachLogLevel = numberOfEntriesForEachLogLevel
            };

            return statistics;
        }

        public EventLogService(EventLogContext eventLogContext)
        {
            _eventLogContext = eventLogContext;
        }

        private readonly EventLogContext _eventLogContext;
    }
}