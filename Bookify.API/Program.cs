using Bookify.API.Extensions;
using Bookify.Application;
using Bookify.Infrastructure;

namespace Bookify.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplicationService();
        builder.Services.AddInfrastrucutreService(builder.Configuration);
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json" , "v1"));
            app.MapOpenApi();
            // app.SeedData();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCustomExceptionHandler();
        app.MapControllers();

        app.Run();
    }
}