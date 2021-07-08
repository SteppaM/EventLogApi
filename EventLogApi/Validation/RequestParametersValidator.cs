using Application.Dtos;
using FluentValidation;

namespace EventLogApi.Validation
{
    public class RequestParametersValidator : AbstractValidator<LogEntryQueryParameters>
    {
        public RequestParametersValidator()
        {
        }
    }
}