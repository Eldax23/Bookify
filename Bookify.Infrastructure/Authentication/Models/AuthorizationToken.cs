using System.Text.Json.Serialization;

namespace Bookify.Infrastructure.Authentication.Models;

public class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}