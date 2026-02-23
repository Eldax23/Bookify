using Bogus;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Bookify.Application.Exceptions.ValidationException;

namespace Bookify.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex , "Exception Occured: {Message}" , ex.Message);
            ExceptionDetails exceptionDetails = GetExceptionDetails(ex);
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Status = exceptionDetails.Status,
                Detail = exceptionDetails.Detail,
            };

            if (exceptionDetails.Errors != null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }
            
            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            // check whether its a validation exception or an internal one
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "Validation Failure",
                "Validation Error",
                "One Or More validation errors has been occured",
                validationException.Errors),
            
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "Internal Server Error",
                "An Unexpected Error has occured",
                null),
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}