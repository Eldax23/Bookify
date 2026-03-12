using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.LoginUser;

public record LoginUserCommand(string Username , string Password) : ICommand<AccessToken>;