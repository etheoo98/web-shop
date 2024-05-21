using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateDiscountDto
{
    [JsonPropertyName("product-id")]
    public int ProductId { get; set; }
    
    [JsonPropertyName("percent")]
    public int Percent { get; set; }
    
    [JsonPropertyName("start-date")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("end-date")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("discounted-price")]
    public decimal DiscountedPrice { get; set; }
}