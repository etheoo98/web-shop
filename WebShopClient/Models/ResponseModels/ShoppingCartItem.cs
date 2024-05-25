using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class ShoppingCartItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [JsonPropertyName("product-name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("stock-quantity")]
        public int StockQuantity { get; set; }

        [JsonPropertyName("discounted-price")]
        public decimal DiscountedPrice { get; set; }

        [JsonPropertyName("total-price")]
        public decimal TotalPrice => DiscountedPrice != 0 ? DiscountedPrice * Quantity : Price * Quantity;
    }
}
