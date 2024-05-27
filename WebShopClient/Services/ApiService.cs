namespace WebShopClient.Services;

public class ApiService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
{
    private readonly HttpClient _client = clientFactory.CreateClient("API Client");

    public HttpClient GetHttpClient()
    {
        var token = httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token)) return _client;
        _client.DefaultRequestHeaders.Remove("Authorization");
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return _client;
    }
}