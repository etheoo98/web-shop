using System.Text.Json;
using WebShopClient.Models.ResponseModels;

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

                //// Debugging
                //Console.WriteLine("JSON Response: " + jsonstring);

                var products = JsonSerializer.Deserialize<List<Product>>(jsonstring);

                return products;

            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<Product?> GetProductAsync(int? id)
        {
            try
            {
                var response = await _client.GetAsync($"Products/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(jsonString); // Bytte till System Json

                //var product = JsonConvert.DeserializeObject<Product>(jsonString); <-- Gamla

                return product;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

