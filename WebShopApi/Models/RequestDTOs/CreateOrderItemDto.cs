using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
