namespace WebShopClient.Services
{
    public class ApiServices
    {
        private readonly HttpClient _client;

        public ApiServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }
    }
}
