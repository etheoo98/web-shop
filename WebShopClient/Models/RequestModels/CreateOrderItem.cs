using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class CreateOrderItem
    {
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
