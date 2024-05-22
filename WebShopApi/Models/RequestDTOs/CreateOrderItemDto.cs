using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs
{
    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = "\'product-id\' is required.")]
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "\'quantity\' is required.")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
