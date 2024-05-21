using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class ShipmentDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("shipped-date")]
    public DateTime ShippedDate { get; set; }
    
    [JsonPropertyName("delivery-date")]
    public DateTime? DeliveryDate { get; set; }

    [JsonPropertyName("shipping-address")]
    public ShippingAddressDto ShippingAddress { get; set; }
}