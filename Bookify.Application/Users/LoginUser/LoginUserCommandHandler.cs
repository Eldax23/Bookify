using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessToken>
{
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> token =
            await _jwtService.GetAccessTokenAsync(request.Username, request.Password, cancellationToken);

        if (token.IsFailure)
            return Result.Failure<AccessToken>(UserErrors.InvalidCredentials);

        return new AccessToken(token.Value);
    }
}