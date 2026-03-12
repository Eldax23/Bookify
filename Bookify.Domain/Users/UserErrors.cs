using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users;

public class UserErrors
{
    public static Error NotFound = new Error("User.NotFound", "This User cannot be found");
    public static Error AuthenticationFailed = new Error("User.NotAuthenticated", "this user cannot be authenticated");
    public static Error InvalidCredentials = new Error("User.InvalidCredentials", "these credentials are invalid");
};