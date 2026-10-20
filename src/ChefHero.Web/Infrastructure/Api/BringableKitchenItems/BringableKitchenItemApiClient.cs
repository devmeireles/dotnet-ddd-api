using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ChefHero.Web.Infrastructure.Api.BringableKitchenItems;

public sealed class BringableKitchenItemApiClient(HttpClient httpClient)
{
    private const string ResourcePath = "bringable-kitchen-items";

    public Task<PagedBringableKitchenItemsResponse> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isActive,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        List<string> query = new List<string>
        {
            $"page={page}",
            $"pageSize={pageSize}"
        };

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query.Add($"searchTerm={Uri.EscapeDataString(searchTerm)}");
        }

        if (isActive.HasValue)
        {
            query.Add($"isActive={isActive.Value.ToString().ToLowerInvariant()}");
        }

        string path = $"{ResourcePath}?{string.Join("&", query)}";
        HttpRequestMessage request = CreateRequest(HttpMethod.Get, path, bearerToken);

        return SendForJsonAsync<PagedBringableKitchenItemsResponse>(
            request,
            cancellationToken);
    }

    public Task<BringableKitchenItemResponse> GetByIdAsync(
        Guid id,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = CreateRequest(
            HttpMethod.Get,
            $"{ResourcePath}/{id}",
            bearerToken);

        return SendForJsonAsync<BringableKitchenItemResponse>(
            request,
            cancellationToken);
    }

    public Task<BringableKitchenItemResponse> CreateAsync(
        CreateBringableKitchenItemRequest body,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Post, ResourcePath, bearerToken);
        request.Content = JsonContent.Create(body);

        return SendForJsonAsync<BringableKitchenItemResponse>(
            request,
            cancellationToken);
    }

    public Task<BringableKitchenItemResponse> PatchAsync(
        Guid id,
        PatchBringableKitchenItemRequest body,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = CreateRequest(
            HttpMethod.Patch,
            $"{ResourcePath}/{id}",
            bearerToken);

        request.Content = JsonContent.Create(body);

        return SendForJsonAsync<BringableKitchenItemResponse>(
            request,
            cancellationToken);
    }

    public Task ActivateAsync(
        Guid id,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = CreateRequest(
            HttpMethod.Post,
            $"{ResourcePath}/{id}/activate",
            bearerToken);

        return SendForNoContentAsync(request, cancellationToken);
    }

    public Task DeactivateAsync(
        Guid id,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = CreateRequest(
            HttpMethod.Post,
            $"{ResourcePath}/{id}/deactivate",
            bearerToken);

        return SendForNoContentAsync(request, cancellationToken);
    }

    private static HttpRequestMessage CreateRequest(
        HttpMethod method,
        string path,
        string bearerToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(bearerToken);

        HttpRequestMessage request = new HttpRequestMessage(method, path);
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", bearerToken);

        return request;
    }

    private async Task<TResponse> SendForJsonAsync<TResponse>(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        using (request)
        using (HttpResponseMessage response = await httpClient.SendAsync(
            request,
            cancellationToken))
        {
            await EnsureSuccessAsync(response, cancellationToken);

            return await response.Content.ReadFromJsonAsync<TResponse>(
                       cancellationToken: cancellationToken)
                   ?? throw new InvalidOperationException(
                       "The API returned an empty response body.");
        }
    }

    private async Task SendForNoContentAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        using (request)
        using (HttpResponseMessage response = await httpClient.SendAsync(
            request,
            cancellationToken))
        {
            await EnsureSuccessAsync(response, cancellationToken);
        }
    }

    private static async Task EnsureSuccessAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        string detail = await response.Content.ReadAsStringAsync(cancellationToken);

        throw new HttpRequestException(
            string.IsNullOrWhiteSpace(detail)
                ? $"API request failed with HTTP {(int)response.StatusCode} ({response.StatusCode})."
                : $"API request failed with HTTP {(int)response.StatusCode} ({response.StatusCode}): {detail}",
            inner: null,
            statusCode: response.StatusCode);
    }
}
