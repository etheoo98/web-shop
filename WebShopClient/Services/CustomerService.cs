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
			var response = await _apiServices.GetHttpClient().GetAsync("api/customers");
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<ICollection<Customer>>();
		}

		public async Task<bool> CreateCustomerAsync(CreateCustomer createCustomer)
		{
			var response = await _apiServices.GetHttpClient().PostAsJsonAsync("api/customers", createCustomer);
			return response.IsSuccessStatusCode;
		}
	}
}
