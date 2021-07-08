using Application.Dtos;
using FluentValidation;

namespace EventLogApi.Validation
{
    public class LogEntryValidator : AbstractValidator<LogEntryDto>
    {
        public LogEntryValidator()
        {
            RuleFor(m => m.EventSourceName).NotEmpty();
            RuleFor(m => m.EventDescription).NotEmpty();
            RuleFor(m => m.TimeStamp).NotEmpty();
        }
    }
}