using Bookify.Application.Users.LoginUser;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(string username, string password,
        CancellationToken cancellationToken = default);
}