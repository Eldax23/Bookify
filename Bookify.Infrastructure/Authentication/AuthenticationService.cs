using System.Net.Http.Json;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;

namespace Bookify.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterAsync(User user, string Username ,  string password, CancellationToken cancellationToken = default)
    {
        UserRepresentationModel userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.credentials = new[]
        {
            new CredentialsRepresentationModel()
            {
                Type = "password",
                Value = password,
                Temporary = false
            }
        };

        userRepresentationModel.Username = Username;

        HttpResponseMessage response =
            await _httpClient.PostAsJsonAsync("users", userRepresentationModel, cancellationToken);
        
        return ExtractIdentityIdFromLocationHeader(response);
    }

    private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage response)
    {
        const string userSegmentName = "users/";

        string locationHeader = response.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header can't be null");
        }

        int indexOfUserSegment = locationHeader.IndexOf(userSegmentName, StringComparison.InvariantCultureIgnoreCase);


        string identityId = locationHeader.Substring(indexOfUserSegment + userSegmentName.Length);

        return identityId;
    }
}