using System.Text.Json;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class DiscountService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : ApiService(clientFactory, httpContextAccessor)
    {
        // GET all discounts
        public async Task<List<Discount>> GetDiscountsAsync()
        {
            try
            {
                var response = await GetHttpClient().GetAsync("Discounts");
                if (!response.IsSuccessStatusCode) return [];
                var jsonString = await response.Content.ReadAsStringAsync();
                var discounts = JsonSerializer.Deserialize<List<Discount>>(jsonString);
                return discounts;

            }
            catch (Exception)
            {
                return [];
            }
        }

        // GET Discount by ID
        public async Task<Discount?> GetDiscountAsync(int? id)
        {
            try
            {
                var response = await GetHttpClient().GetAsync($"Discounts/{id}");
                if (!response.IsSuccessStatusCode) return null;
                var jsonString = await response.Content.ReadAsStringAsync();
                var discount = JsonSerializer.Deserialize<Discount>(jsonString);
                return discount;

            }
            catch (Exception)
            {
                return null;
            }
        }

        // Create Discount
        public async Task<bool> CreateDiscountAsync(CreateDiscount createDiscount)
        {
            var response = await GetHttpClient().PostAsJsonAsync("Discounts", createDiscount);
            return response.IsSuccessStatusCode;
        }
    }
}
