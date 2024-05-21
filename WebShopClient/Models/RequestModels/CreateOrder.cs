using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Models.RequestModels
{
    public class CreateOrder
    {
        [JsonPropertyName("total-sum")]
        public decimal TotalSum { get; set; }

        [JsonPropertyName("order-date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("is-paid")]
        public bool IsPaid { get; set; }

        [JsonPropertyName("customer-id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("shipment")]
        public Shipment? Shipment { get; set; }

        [JsonPropertyName("product-ids")]
        public ICollection<int> ProductIds { get; set; }
    }
}
