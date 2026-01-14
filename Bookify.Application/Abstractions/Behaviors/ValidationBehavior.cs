using Bookify.Application.Abstractions.Messaging;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Bookify.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest , TResponse> : IPipelineBehavior<TRequest , TResponse>
    where TRequest : IBaseCommand // since we only wanna apply validation to our commands
{
    
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();


        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        
        List<ValidationError> validationErrors = _validators.
            Select(validator => validator.Validate(context))
            .Where(res => res.Errors.Any())
            .SelectMany(validationRes => validationRes.Errors)
            .Select(validationFailure => new ValidationError(validationFailure.PropertyName , validationFailure.ErrorMessage))
            .ToList();


        if (validationErrors.Any())
            throw new Bookify.Application.Exceptions.ValidationException(validationErrors);
        
        return await next();
    }
}