using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Todo.GravatarConnector;

public class GravatarProfileService : IProfileService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GravatarProfileService> _logger;

    public GravatarProfileService(HttpClient httpClient, ILogger<GravatarProfileService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<GravatarProfile?> GetProfileAsync(string profileIdentifier)
    {
        var url = $"profiles/{profileIdentifier}";
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.GetAsync(url);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the profile.");
            return null; // Return null if there is a network or other error.
        }

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GravatarProfile>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.NotFound:
                return null; // Return null if profile is not found.
            case System.Net.HttpStatusCode.TooManyRequests:
                _logger.LogWarning("API rate limit exceeded.");
                return null;
            case System.Net.HttpStatusCode.InternalServerError:
                _logger.LogError("An error occurred on the server.");
                return null;
            default:
                var errorMessage = $"Unexpected status code: {response.StatusCode}";
                _logger.LogError(errorMessage);
                throw new HttpRequestException(errorMessage);
        }
    }
}