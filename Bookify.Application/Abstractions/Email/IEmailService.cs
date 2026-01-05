namespace Bookify.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.Records.Email recipeint, string subject, string body);
}