using Newtonsoft.Json;
using WebShopClient.Models.ResponseVMs;

namespace WebShopClient.Services
{
    public class ProductServices
    {
        private readonly HttpClient _client;

        public ProductServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }

        // GET all products
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _client.GetAsync("Products");
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Product>();
                }
                var jsonstring = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonstring);
                return products;

            }
            catch (Exception ex)
            {
                return new List<Product>();
            }
        }
    }
}

