using Newtonsoft.Json;
using WebShopClient.Models.RequestModels;
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
                var product = JsonConvert.DeserializeObject<Product>(jsonString);
                return product;

            }
            catch (Exception ex)
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
                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
                return categories;
            }
            catch (Exception ex)
            {
                return new List<Category>();
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

