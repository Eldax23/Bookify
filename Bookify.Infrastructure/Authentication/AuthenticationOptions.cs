namespace Bookify.Infrastructure.Authentication;

public class AuthenticationOptions
{
    public string Audience { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string MetadataUrl { get; init; } = string.Empty;
    public bool RequireHttpsMetaData { get; init; } = false;
}