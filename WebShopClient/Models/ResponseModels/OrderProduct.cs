using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class OrderProduct
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("order")]
        public Order Order { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }
    }
}
