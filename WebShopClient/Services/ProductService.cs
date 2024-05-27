using Azure;
using System.Text.Json;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class ProductService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : ApiService(clientFactory, httpContextAccessor)
    {

        // GET all products
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await GetHttpClient().GetAsync("Products");
                if (!response.IsSuccessStatusCode) return [];
                var jsonString = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(jsonString);
                return products;

            }
            catch (Exception)
            {
                return [];
            }
        }
        //GET Product by ID
        public async Task<Product?> GetProductAsync(int? id)
        {
            try
            {
                var response = await GetHttpClient().GetAsync($"Products/{id}");
                if (!response.IsSuccessStatusCode) return null;
                var jsonString = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(jsonString);
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
            var response = await GetHttpClient().PostAsJsonAsync("Products", createProduct);
            return response.IsSuccessStatusCode;
        }
        //Update Product
        public async Task<bool> UpdateProductAsync(EditProduct editProduct)
        {
            var response = await GetHttpClient().PutAsJsonAsync($"Products/{editProduct.Id}", editProduct);
            if (response.IsSuccessStatusCode) return response.IsSuccessStatusCode;
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {content}");
            return response.IsSuccessStatusCode;
        }
        //Delete Product
        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var response = await GetHttpClient().DeleteAsync($"Products/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product {ex.Message}");
                return false;
            }
        }

        //GET Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var response = await GetHttpClient().GetAsync("Categories");
                if (!response.IsSuccessStatusCode) return [];
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(jsonString);
                return categories;
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}

