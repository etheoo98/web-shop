using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class ShipmentDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("shipped-date")]
    public DateTime ShippedDate { get; set; } = DateTime.Now.AddDays(1);
    
    [JsonPropertyName("delivery-date")]
    public DateTime? DeliveryDate => ShippedDate.AddDays(5);

    [JsonPropertyName("shipping-address")]
    public ShippingAddressDto ShippingAddress { get; set; }
}