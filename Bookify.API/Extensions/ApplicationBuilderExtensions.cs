using Bookify.API.Middlewares;
using Bookify.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        dbContext.Database.Migrate();
    }

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}