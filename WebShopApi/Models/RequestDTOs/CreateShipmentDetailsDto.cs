using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs
{
    public class CreateShipmentDetailsDto
    {
        [Required(ErrorMessage = "\'shipped-date\' is required.")]
        [JsonPropertyName("shipped-date")]
        public DateTime? ShippedDate { get; set; }

        [Required(ErrorMessage = "\'delivery-date\' is required.")]
        [JsonPropertyName("delivery-date")]
        public DateTime? DeliveryDate { get; set; } 

        [Required(ErrorMessage = "\'shipping-address\' is required.")]
        [JsonPropertyName("shipping-address")]
        public CreateShippingAddressDto ShippingAddress { get; set; }
    }
}
