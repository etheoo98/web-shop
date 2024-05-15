using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class ApiServices
    {
        private readonly HttpClient _client;

        public ApiServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }
        
        public HttpClient GetHttpClient()
        {
            return _client;
        }
    }
}
