using System.Net.Http.Json;

namespace ChefHero.Web.Infrastructure.Api.Authentication;

public sealed class AuthApiClient(HttpClient httpClient)
{
    public async Task<LoginApiResponse> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            "auth/login",
            new LoginApiRequest(email, password),
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string detail = await response.Content.ReadAsStringAsync(cancellationToken);

            throw new HttpRequestException(
                string.IsNullOrWhiteSpace(detail)
                    ? $"Login failed with HTTP {(int)response.StatusCode} ({response.StatusCode})."
                    : $"Login failed with HTTP {(int)response.StatusCode} ({response.StatusCode}): {detail}",
                inner: null,
                statusCode: response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<LoginApiResponse>(
                   cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException(
                   "The API returned an empty login response.");
    }
}
