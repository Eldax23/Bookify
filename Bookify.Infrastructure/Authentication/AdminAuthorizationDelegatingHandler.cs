using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

// this is like a wrapper for http request to send in the access token on each http request made by HttpClient.
public class AdminAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IOptions<KeycloakOptions> _keycloakOptions;

    public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> options)
    {
        _keycloakOptions = options;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        AuthorizationToken authorizationToken = await GetAuthorizationToken(cancellationToken);
        
        // with every HttpRequest send the authorizationToken along with it
        request.Headers.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme, authorizationToken.AccessToken);
        
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak error: {response.StatusCode} - {error}");
        }
        
        return response;
    }

    private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        KeyValuePair<string, string>[] authorizationParamaters = new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new("client_id", _keycloakOptions.Value.AdminClientId),
            new("client_secret", _keycloakOptions.Value.AdminClientSecret),
            new("scope", "openid"),
        };

        FormUrlEncodedContent authorizationRequestContent = new FormUrlEncodedContent(authorizationParamaters);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
            new Uri(_keycloakOptions.Value.TokenUrl))
        {
            Content = authorizationRequestContent
        };


        // GO FETCH THAT TOKEN!!
        HttpResponseMessage authorizationResponse = await base.SendAsync(request, cancellationToken);
        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>() ??
               throw new ApplicationException("Authorization token returned null");
    }
}