using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Models.RequestModels;

namespace WebShopClient.Models.RequestModels
{
    public class CreateOrder
    {       
        [JsonPropertyName("total-sum")]
        public decimal TotalSum { get; set; }

        [JsonPropertyName("is-paid")]
        public bool IsPaid { get; set; } = true;

        [JsonPropertyName("order-items")]
        public ICollection<CreateOrderItem> OrderItems { get; set; }

        [JsonPropertyName("shipment-details")]
        public Shipment ShipmentDetails { get; set; }

    }
}
