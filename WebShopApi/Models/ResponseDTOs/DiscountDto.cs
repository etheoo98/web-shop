using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class DiscountDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("percent")]
    public int Percent { get; set; }
    
    [JsonPropertyName("start-date")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("end-date")]
    public DateTime EndDate { get; set; }
}