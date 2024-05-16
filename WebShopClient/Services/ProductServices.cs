using Newtonsoft.Json;
using System;
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
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonstring);
                return products;

            }
            catch (Exception ex)
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
                var product = JsonConvert.DeserializeObject<Product>(jsonString);
                return product;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            try
            {
                var response = await _client.GetAsync($"Products/Filter?category={category}");
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

