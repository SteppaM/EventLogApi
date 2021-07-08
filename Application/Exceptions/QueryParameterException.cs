using System;

namespace Application.Exceptions
{    
    public class QueryParameterException : Exception
    {
        public string Description { get; }

        public QueryParameterException(string description)
        {
            Description = description;
        }
    }
}