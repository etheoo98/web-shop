using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs
{
    public class OrderProductDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("order")]
        public OrderDto Order { get; set; }

        [JsonPropertyName("product")]
        public ProductDto Product { get; set; }
    }
}
