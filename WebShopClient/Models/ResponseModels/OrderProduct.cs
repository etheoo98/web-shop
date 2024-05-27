using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class OrderProduct
    {
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [JsonPropertyName("product-name")]
        public string ProductName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("discounted-price")]
        public decimal DiscountedPrice { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("order")]
        public Order Order { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }           
    }
}
