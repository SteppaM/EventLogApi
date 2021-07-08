using System;
using System.Collections.Generic;
using Persistence.Models;

namespace Application.Dtos
{
    public class LogEntryQueryParameters
    {
        public DateTime FromDateTime { get; set; } = DateTime.MinValue;
        public DateTime ToDateTime { get; set; } = DateTime.MaxValue;
        public string SearchString { get; set; } = "";
        public List<LogLevel> LogLevels { get; set; } = new List<LogLevel>();
    }
}