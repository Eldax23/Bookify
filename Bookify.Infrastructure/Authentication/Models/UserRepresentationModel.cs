using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Authentication.Models;

public class UserRepresentationModel
{
    public Dictionary<string, string> Access { get; set; }
    public Dictionary<string, List<string>> Attributes { get; set; }
    public Dictionary<string, string> ClientRoles { get; set; }
    public long? CreatedTimeStamp { get; set; }
    public CredentialsRepresentationModel[] Credentials { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public bool Enabled { get; set; }
    public string Id { get; set; }
    public string[] Groups { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Origin { get; set; }
    public string[] RealmRoles { get; set; }
    public string[] RequiredActions { get; set; }
    public string Self { get; set; }
    public string ServiceAccountClientId  { get; set; }
    public string Username { get; set; }

    internal static UserRepresentationModel FromUser(User user)
        => new()
        {
            FirstName = user.FirstName.Value,
            LastName = user.LastName.Value,
            Email = user.Email.Value,
            EmailVerified = true,
            CreatedTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Attributes = new  Dictionary<string, List<string>>(),
            RequiredActions = Array.Empty<string>(),
        };
}