using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs
{
    public class OrderProductDto
    {
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [JsonPropertyName("product-name")]
        public string ProductName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("discounted-price")]
        public decimal? DiscountedPrice { get; set; }
    }
}
