using Bookify.Domain.Users;

namespace Bookify.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user, string Username , string password, CancellationToken cancellationToken = default);
}