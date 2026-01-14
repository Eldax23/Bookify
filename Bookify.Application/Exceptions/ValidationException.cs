using Bookify.Application.Abstractions.Behaviors;

namespace Bookify.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> validationErrors)
    {
        Errors = validationErrors;
    }
    
    public IEnumerable<ValidationError> Errors { get;  }
}