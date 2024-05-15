using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
