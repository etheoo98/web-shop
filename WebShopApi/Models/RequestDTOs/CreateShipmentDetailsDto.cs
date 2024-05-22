using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShop.Models.ResponseDTOs;

namespace WebShop.Models.RequestDTOs
{
    public class CreateShipmentDetailsDto
    {
        [JsonPropertyName("shipped-date")]
        public DateTime? ShippedDate { get; set; }

        [JsonPropertyName("delivery-date")]
        public DateTime? DeliveryDate { get; set; } 

        [Required]
        [JsonPropertyName("shipping-address")]
        public CreateShippingAddressDto ShippingAddress { get; set; }
    }
}
