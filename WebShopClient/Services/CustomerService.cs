using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class CustomerService
	{
		private readonly ApiServices _apiServices;

        public CustomerService(ApiServices apiServices)
        {
            _apiServices = apiServices;
        }

		public async Task<ICollection<Customer>> GetCustomersAsync()
		{
			var response = await _apiServices.GetHttpClient().GetAsync("customers");
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<ICollection<Customer>>();
		}
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var response = await _apiServices.GetHttpClient().GetAsync($"customers/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task<bool> CreateCustomerAsync(CreateCustomer createCustomer)
		{
			var response = await _apiServices.GetHttpClient().PostAsJsonAsync("customers", createCustomer);
			return response.IsSuccessStatusCode;
		}
        public async Task<bool> UpdateCustomerAsync(int id, UpdateCustomer updateCustomer)
        {
            var response = await _apiServices.GetHttpClient().PutAsJsonAsync($"customers/{id}", updateCustomer);
            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> AttemptLogin(LoginCustomer model)
        {
	        var response = await _apiServices.GetHttpClient().PostAsJsonAsync("authenticators", model);

	        if (!response.IsSuccessStatusCode) return false;
	        
	        var token = await response.Content.ReadAsStringAsync();
	        // Store token in Session
	        _httpContextAccessor.HttpContext?.Session.SetString("JwtToken", token);

	        return true;
        }

        public async Task<Customer?> GetCurrentUser()
        {
	        var response = await _apiServices.GetHttpClient().GetAsync("customers/0"); // 0 returns current customer
	        if (!response.IsSuccessStatusCode) return null;
	        
	        return await response.Content.ReadFromJsonAsync<Customer>();
        }
    }
}
