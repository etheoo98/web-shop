using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Models.RequestModels;

namespace WebShopClient.Models.RequestModels
{
    public class CreateOrder
    {
        [JsonPropertyName("customer-id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("order-items")]
        public ICollection<CreateOrderItem> OrderItems { get; set; }

        [JsonPropertyName("shipment-details")]
        public Shipment ShipmentDetails { get; set; }
     
    }
}
