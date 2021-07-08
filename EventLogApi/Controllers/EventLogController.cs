using System.Collections.Generic;
using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventLogApi.Controllers
{
    [ApiController]
    [Route("logEntries")]
    public class EventLogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLogEntries([FromQuery] LogEntryQueryParameters queryParameters)
        {
            var logEntries = _eventLogService.GetLogEntries(queryParameters);
            return Ok(logEntries);
        }
        
        [HttpPost]
        public IActionResult AddLogEntry([FromBody]LogEntryDto logEntryDto)
        {
            var eventId = _eventLogService.AddLogEntry(logEntryDto);
            return Ok(eventId);
        }
        
        [HttpDelete]
        public IActionResult RemoveLogEntries([FromBody]List<int> entryIds)
        {
            var numberOfRemovedEntries = _eventLogService.RemoveLogEntries(entryIds);
            return Ok(numberOfRemovedEntries);
        }
        
        [HttpGet]
        [Route("/statistics")]
        public IActionResult GetEventLogStatistics()
        {
            var statistics = _eventLogService.GetStatistics();
            return Ok(statistics);
        }
        
        public EventLogController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }
        
        private readonly IEventLogService _eventLogService;
    }
}