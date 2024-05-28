using System.Text.Json;
using System.Text.Json.Serialization;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class CategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }
        public async Task<bool> CreateCategoryAsync(CreateCategory createCategory)
        {
            var response = await _client.PostAsJsonAsync("Categories", createCategory);
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
