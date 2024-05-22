using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class OrderService
    {
        private readonly ApiServices _api;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ApiServices api, ILogger<OrderService> logger)
        {
            _api = api;
            _logger = logger;
        }

        public async Task<ICollection<Order>> GetOrdersAsync()
        {
            try
            {
                var response = await _api.GetHttpClient().GetAsync("orders");
                response.EnsureSuccessStatusCode();
                var orders = await response.Content.ReadFromJsonAsync<ICollection<Order>>();

                if (orders != null && orders.Count != 0)
                {
                    return orders;
                }
                else
                {
                    _logger.LogWarning("No orders found.");
                    return new List<Order>();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error fetching orders: {ex.Message}");
                throw;
            }
        }      

        public async Task<bool> CreateOrderAsync(CreateOrder order)
        {
            try
            {
                var response = await _api.GetHttpClient().PostAsJsonAsync<CreateOrder>("orders", order);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Order created successfully.");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Failed to create order. Status code: {response.StatusCode}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error creating order: {ex.Message}");
                throw;
            }
        }
    }
}
