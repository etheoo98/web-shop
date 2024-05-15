using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }
        
        public HttpClient GetHttpClient()
        {
            return _client;
        }
    }
}
