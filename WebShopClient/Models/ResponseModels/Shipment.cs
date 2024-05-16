using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseModels
{
    public class Shipment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("shipped-date")]
        public DateTime ShippedDate { get; set; }

        [JsonPropertyName("delivery-date")]
        public DateTime? DeliveryDate { get; set; }
    }
}
