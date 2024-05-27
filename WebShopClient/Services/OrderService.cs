using System.Text.Json;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class OrderService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, ILogger<OrderService> logger) : ApiService(clientFactory, httpContextAccessor)
    {
        public async Task<ICollection<Order>> GetOrdersAsync()
        {
            try
            {
                var response = await GetHttpClient().GetAsync("orders");
                response.EnsureSuccessStatusCode();
                var orders = await response.Content.ReadFromJsonAsync<ICollection<Order>>();
                if (orders != null && orders.Count != 0) return orders;
                logger.LogWarning("No orders found.");
                return new List<Order>();
            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"Error fetching orders: {ex.Message}");
                throw;
            }
        }

        public async Task<Order?> GetOrderByIdAsync(int? id)
        {
            try
            {
                var response = await GetHttpClient().GetAsync($"orders/{id}");
                if (!response.IsSuccessStatusCode) return null;
                var jsonString = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<Order>(jsonString);
                if (order != null) return order;
                logger.LogWarning($"Order with ID: {id} not found.");
                return null;

            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"Error fetching order with ID {id}: {ex.Message}");
                throw new Exception($"An error occurred while fetching the order with ID {id}.", ex);
            }
            catch (Exception ex)
            {
                logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<int?> CreateOrderAsync(CreateOrder order)
        {
            try
            {
                var response = await GetHttpClient().PostAsJsonAsync("orders", order);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning($"Failed to create order. Status code: {response.StatusCode}");
                    return null;
                }
                
                var jsonString = await response.Content.ReadAsStringAsync();
                var createdOrder = JsonSerializer.Deserialize<Order>(jsonString);
                logger.LogInformation("Order created successfully.");
                return createdOrder?.Id;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"Error creating order: {ex.Message}");
                throw;
            }
        }
    }
}
