using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Models.RequestModels
{
    public class CreateShipment
    {
        [JsonPropertyName("shipped-date")]
        public DateTime? ShippedDate { get; set; }

        [JsonPropertyName("delivery-date")]
        public DateTime? DeliveryDate { get; set; }

        [Required]
        [JsonPropertyName("shipping-address")]
        public CreateShipmentAddress ShippingAddress { get; set; }
    }
}
