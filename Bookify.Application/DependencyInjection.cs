using Bookify.Application.Abstractions.Behaviors;
using Bookify.Domain.Bookings.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient<PricingService>();
        return services;
    }
}