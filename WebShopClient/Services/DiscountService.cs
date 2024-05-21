using System.Text.Json;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class DiscountService
    {
        private readonly HttpClient _client;

        public DiscountService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }

        // GET all discounts
        public async Task<List<Discount>> GetDiscountsAsync()
        {
            try
            {
                var response = await _client.GetAsync("Discounts");
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Discount>();
                }

                var jsonstring = await response.Content.ReadAsStringAsync();


                var discounts = JsonSerializer.Deserialize<List<Discount>>(jsonstring);

                return discounts;

            }
            catch (Exception)
            {
                return new List<Discount>();
            }
        }

        // GET Discount by ID
        public async Task<Discount?> GetDiscountAsync(int? id)
        {
            try
            {
                var response = await _client.GetAsync($"Discounts/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
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
            var response = await _client.PostAsJsonAsync("Discounts", createDiscount);
            return response.IsSuccessStatusCode;
        }
    }
}
