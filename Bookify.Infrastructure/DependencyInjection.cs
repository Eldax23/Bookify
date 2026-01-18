using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Email;
using Bookify.Infrastructure.Clock;
using Bookify.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucutreService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService , EmailService>();
        var connectionstring = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new ArgumentNullException();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionstring).UseSnakeCaseNamingConvention();
        });
        return services;
    }
}