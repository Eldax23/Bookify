using System.Windows.Input;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest , TResponse> : IPipelineBehavior<TRequest , TResponse>
    where TRequest : ICommand  // since we only wanna log our commands , cuz if we log queries it will make them slower
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string name = request.GetType().Name; // extracting the name of the current command
        try
        {
            _logger.LogInformation("Executing Command {command}" , name);
            TResponse result = await next();
            _logger.LogInformation("Command {command} has been successfully executed" , name);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e , "Command {command} execution failed" , name);
            throw;
        }
    }
}