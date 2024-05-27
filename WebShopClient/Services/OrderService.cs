using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
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

        public async Task<Order?> GetOrderByIdAsync(int? id)
        {
            try
            {
                var response = await _api.GetHttpClient().GetAsync($"orders/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var order = await response.Content.ReadFromJsonAsync<Order>();            

                if (order == null)
                {
                    _logger.LogWarning($"Order with ID: {id} not found.");
                    return null;
                }

                return order;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error fetching order with ID {id}: {ex.Message}");
                throw new Exception($"An error occurred while fetching the order with ID {id}.");
            }      
        }

        public async Task<bool> CreateOrderAsync(CreateOrder createOrder)
        {
            var response = await _api.GetHttpClient().PostAsJsonAsync("Orders", createOrder);
            return response.IsSuccessStatusCode;
        }    
    }
}
