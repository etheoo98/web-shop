using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class OrderDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("customer-id")]
    public int CustomerId { get; set; }
    
    [JsonPropertyName("total-sum")]
    public decimal TotalSum { get; set; }
    
    [JsonPropertyName("order-date")]
    public DateTime OrderDate { get; set; }
    
    [JsonPropertyName("is-paid")]
    public bool IsPaid { get; set; }
    
    [JsonPropertyName("shipment-details")]
    public ShipmentDto? ShipmentDto { get; set; }

    [JsonPropertyName("products")]
    public ICollection<ProductDto> ProductDtos { get; set; } = [];
}