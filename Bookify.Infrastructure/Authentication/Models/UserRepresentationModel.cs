using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Authentication.Models;

public class UserRepresentationModel
{
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public bool Enabled { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public CredentialsRepresentationModel[] credentials { get; set; }

    internal static UserRepresentationModel FromUser(User user)
        => new()
        {
            FirstName = user.FirstName.Value,
            LastName = user.LastName.Value,
            Email = user.Email.Value,
            EmailVerified = true,
            Enabled = true,
        };
}