using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs
{
    public class CustomerOrderDto
    {
        [JsonPropertyName("customer-id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("order-id")]
        public int OrderId { get; set; }
    }
}
