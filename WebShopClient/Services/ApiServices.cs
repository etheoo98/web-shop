using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseVMs;

namespace WebShopClient.Services
{
    public class ApiServices
    {
        private readonly HttpClient _client;

        public ApiServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }

        public async Task<ICollection<Customer>> GetCustomersAsync()
        {
            var response = await _client.GetAsync("api/customers");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ICollection<Customer>>();
        }

        public async Task<bool> CreateCustomerAsync(CreateCustomer createCustomer)
        {
            var response = await _client.PostAsJsonAsync("api/customers", createCustomer);
            return response.IsSuccessStatusCode;
        }

        //public async Task<ICollection<Product>> GetProductsByIdsAsync(ICollection<int> productIds)
        //{
        //    // Anropa API:t för att hämta produktdetaljer baserat på ID:na
        //    // Implementera logik för att göra HTTP-anrop till API:t och hantera svar här
        //}
    }
}
