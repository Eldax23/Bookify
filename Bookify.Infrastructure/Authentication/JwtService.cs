using System.Net.Http.Json;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Behaviors;
using Bookify.Application.Users.LoginUser;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

public class JwtService : IJwtService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<KeycloakOptions> _keycloakOptions;

    public JwtService(HttpClient httpClient , IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions;
        _keycloakOptions = keycloakOptions;
    }
    
    
    public async Task<Result<string>> GetAccessTokenAsync(string username, string password, CancellationToken cancellationToken = default)
    {

        try
        {
            var authRequestParams = new KeyValuePair<string, string>[]
            {
                new("grant_type", "password"),
                new("client_id", _keycloakOptions.Value.AuthClientId),
                new("client_secret", _keycloakOptions.Value.AuthClientSecret),
                new("scope", "openid"),
                new("username", username),
                new("password", password)
            };

            FormUrlEncodedContent authorizationRequestContent = new FormUrlEncodedContent(authRequestParams);

            HttpResponseMessage response =
                await _httpClient.PostAsync("" , authorizationRequestContent);


            response.EnsureSuccessStatusCode();

            AuthorizationToken authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();

            if (authorizationToken is null)
                return Result.Failure<string>(UserErrors.AuthenticationFailed);

            return authorizationToken.AccessToken;

        }
        catch (HttpRequestException e)
        {
            return Result.Failure<string>(UserErrors.AuthenticationFailed);
        }

    }
}