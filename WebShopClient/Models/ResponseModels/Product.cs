using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("add-date")]
        public DateTime AddDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("is-discontinued")]
        public bool IsDiscontinued { get; set; }

        [JsonPropertyName("discount")]
        public Discount? Discount { get; set; }

        [JsonPropertyName("categories")]
        public ICollection<Category> Categories { get; set; }
    }
}
