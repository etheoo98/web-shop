using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class ShoppingCartItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("product-name")]
        public string ProductName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("percent-discount")]
        public int PercentDiscount { get; set; }

        [JsonPropertyName("total-price")]
        public decimal TotalPrice => Price * Quantity;
    }
}
