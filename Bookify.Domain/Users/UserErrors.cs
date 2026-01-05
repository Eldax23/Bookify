using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users;

public class UserErrors
{
    public static Error NotFound = new Error("User.NotFound", "This User cannot be found");
};