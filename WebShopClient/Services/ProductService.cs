using System.Text.Json;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class ProductService
    {
        private readonly HttpClient _client;

        public ProductService(IHttpClientFactory clientFactory)
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
        //GET Product by ID
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
        //Create Product
        public async Task<bool> CreateProductAsync(CreateProduct createProduct)
        {
            var response = await _client.PostAsJsonAsync("Products", createProduct);
            return response.IsSuccessStatusCode;
        }
        

        //Update Product
        public async Task<bool> UpdateProductAsync(EditProduct editProduct)
        {
            var response = await _client.PutAsJsonAsync($"Products/{editProduct.Id}", editProduct);
            return response.IsSuccessStatusCode;
        }
        //GET Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var response = await _client.GetAsync("Categories");
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Category>();
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(jsonString);
                return categories;
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }
    }
}

