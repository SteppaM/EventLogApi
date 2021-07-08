using Microsoft.AspNetCore.Mvc;

namespace EventLogApi.CustomProblemDetails
{
    public class ParametersProblemDetails : ProblemDetails 
    {
        public string AdditionalInfo { get; set; }
    }
}