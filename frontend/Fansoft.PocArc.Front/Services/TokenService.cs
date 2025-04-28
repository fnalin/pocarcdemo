using System.Text;
using System.Text.Json;

namespace Fansoft.PocArc.Front.Services;

public class TokenService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public TokenService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string?> GetTokenAsync()
    {
        var clientId = _configuration["Keycloak:ClientId"];
        var clientSecret = _configuration["Keycloak:ClientSecret"];
        var tokenUrl = _configuration["Keycloak:TokenUrl"];

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(tokenUrl))
            throw new InvalidOperationException("Keycloak credentials are not properly configured.");

        var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

        var body = new StringContent(
            $"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded"
        );

        request.Content = body;

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Error fetching token: {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();

        var json = JsonDocument.Parse(content);
        var accessToken = json.RootElement.GetProperty("access_token").GetString();

        return accessToken;
    }
}