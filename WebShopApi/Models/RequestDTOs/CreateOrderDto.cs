using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace WebShop.Models.RequestDTOs;

public class CreateOrderDto
{
    [Required]
    [JsonPropertyName("customer-id")]
    public int CustomerId { get; set; }
    
    [Required]
    [JsonPropertyName("product-ids")]
    public ICollection<int> ProductIds { get; set; }
}