using System.Net;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class CustomerService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : ApiService(clientFactory, httpContextAccessor)
    {
	    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	    public async Task<ICollection<Customer>> GetCustomersAsync()
		{
			var response = await GetHttpClient().GetAsync("customers");
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<ICollection<Customer>>();
		}
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            var response = await GetHttpClient().GetAsync($"customers/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task<bool> CreateCustomerAsync(CreateCustomer createCustomer)
		{
			var response = await GetHttpClient().PostAsJsonAsync("customers", createCustomer);
			return response.IsSuccessStatusCode;
		}
        public async Task<bool> UpdateCustomerAsync(int id, UpdateCustomer updateCustomer)
        {
            var response = await GetHttpClient().PutAsJsonAsync($"customers/{id}", updateCustomer);
            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> AttemptLogin(LoginCustomer model)
        {
	        var response = await GetHttpClient().PostAsJsonAsync("authenticators", model);

	        if (!response.IsSuccessStatusCode) return false;
	        
	        var token = await response.Content.ReadAsStringAsync();
	        // Store token in Session
	        _httpContextAccessor.HttpContext?.Session.SetString("JwtToken", token);

	        return true;
        }

        public async Task<Customer?> GetCurrentUser()
        {
	        var response = await GetHttpClient().GetAsync("customers/0"); // 0 returns current customer
	        if (!response.IsSuccessStatusCode) return null;
	        
	        return await response.Content.ReadFromJsonAsync<Customer>();
        }
    }
}
