namespace Bookify.Infrastructure.Authentication.Models;

public class CredentialsRepresentationModel
{
    public string Type { get; set; }
    public string Value { get; set; }
    public bool Temporary { get; set; }
}